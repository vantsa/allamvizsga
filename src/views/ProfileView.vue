<template>
  <div>
    <MenuBar />
    <v-spacer />
    <v-card>
      <h1>Profil szerkesztése</h1>
      <v-avatar size="150">
        <v-img src="../assets/profil.jpg"></v-img>
      </v-avatar>
      <h3>vantsaa</h3>
      <h3 class="utolso">alparvantsa@gmail.com</h3>
      <span>Maximális távolság&emsp;&emsp;&emsp;&emsp; {{ value }} km</span>
      <v-slider
        class="slider"
        v-model="value"
        :min="0"
        :max="50"
        :step="1"
        thumb-label
      ></v-slider>
      <span>Korcsoport&emsp;&emsp;&emsp;&emsp; {{ format }} </span>
      <v-range-slider
        class="slider"
        v-model="range"
        :min="16"
        :max="100"
        :step="1"
        thumb-label
      ></v-range-slider>
      <v-card-actions class="justify-end">
        <v-btn rounded class="create-btn" text large>Mentés </v-btn>
      </v-card-actions>
      
    </v-card>
  </div>
</template>

<script>
import MenuBar from "../components/MenuBar.vue";

export default {
  name: "ProfileView",
  components: {
    MenuBar,
  },
  data() {
    return {
      value: 25,
      range: [16,30]
    };
  },
  computed: {
    format() {
        return this.range[0] + ' - ' + this.range[1];
    }
  }
};
</script>

<style scoped>
.v-card {
  width: 50%;
  margin: 0 auto;
  margin-top: 4rem;
}
h1 {
  text-align: center;
  padding: 1rem 0rem;
  border-bottom: 1px solid lightgray;
}
.v-avatar {
  margin: 2rem auto;
  margin-bottom: 1rem;
  display: block;
}
h3 {
  text-align: center;
  color: darkgray;
}
.utolso{
    margin-bottom: 3rem;
}
.slider {
  display: block;
  margin-top: 1rem;
  width: 80%;
  margin: 0 auto;
}
span {
  margin-left: 6rem;
  font-weight: 600;
}
.create-btn {
  background-color: #3e1e68;
  color: white;
  margin: 0 1.5rem 1.5rem 0;
}
</style>