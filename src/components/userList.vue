<template>
  <v-card class="mx-auto" max-width="300">
    <h4> {{ user.username }}</h4>
    <p> {{ user.firstName + " " + user.lastName }}
    <p> {{ user.email }}</p>
    <v-btn rounded @click="onDelete" class="delete-btn"><v-icon>mdi-delete</v-icon></v-btn>
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
  }
};
</script>

<style scoped>
h4, p{
    display: inline;
}
.alert {
  position: fixed;
  top: 5%;
  left: 80%;
  transform: translateX(-50%);
  z-index: 9999;
  width: 40%;
}
</style>
