<template>
  <v-card class="mx-auto">
    <div class="table-container">
    <div class="table-row">
      <div class="table-cell username"><h4>{{ user.username }}</h4></div>
      <div class="table-cell name">{{ user.firstName }} {{ user.lastName }}</div>
      <div class="table-cell email">{{ user.email }}</div>
      <div class="table-cell">
        <v-btn rounded @click="onDelete" class="delete-btn" v-if="isUser"><v-icon>mdi-delete</v-icon></v-btn>
      </div>
    </div>
  </div>
        <v-alert
      fixed
      class="alert"
      type="success"
      v-if="successMsg"
      dismissible
      @input="successMsg = ''"
      >{{ successMsg }}</v-alert
    >
  </v-card>
</template>

<script>
import axios from "axios"

export default {
  name: "userList",
  components: {},
  props: {
    user: {
        type: Object,
        required: true,
    }
  },
  data() {
    return {
        successMsg : "",
        isUser: true,
    };
  },
  methods: {
    async onDelete() {
        try{
            // eslint-disable-next-line no-unused-vars
            const response = await axios.delete(`api/users/${this.user.id}`,
            { headers: {
              "Content-Type": "application/json",
            }});
            window.location.reload();
            this.successMsg = "Sikeres törlés!";        
        }catch (error) {
            this.successMsg = "";
        }
    }
  },
  async mounted() {
    if(this.user.username == "admin")
      {
        this.isUser = false;
      }
  }
};
</script>

<style scoped>
*{
  font-family: 'Baloo-Regular';
}
.alert {
  position: fixed;
  top: 5%;
  left: 80%;
  transform: translateX(-50%);
  z-index: 9999;
  width: 40%;
}
.table-container {
  display: table;
  width: 100%;
  border-collapse: collapse;
}
.mx-auto{
  border-radius: 0px;
}
.table-row {
  display: table-row;
}

.table-cell {
  display: table-cell;
  padding: 10px;
  border-bottom: 1px solid #ddd;
  white-space: nowrap; 
  overflow: hidden; 
  text-overflow: ellipsis; 
  font-size: 1.25rem;
}
.table-cell.username {
  width: 20%; 
}

.table-cell.name {
  width: 30%; 
}

.table-cell.email {
  width: 40%;
}
.table-cell:last-child {
  text-align: right;
}

.delete-btn {
  padding: 0;
  min-width: 0;
}
.mx-auto{
  padding: 1rem;
}
@media only screen and (max-width: 800px){
.table-cell{
  font-size: 0.8rem;
}
}
@media only screen and (max-width: 600px){
.table-cell{
  font-size: 0.7rem;
}
.v-btn:not(.v-btn--round).v-size--default{
  height: 24px;
  min-width: 30px;
}
}
</style>
