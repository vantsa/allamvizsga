<template>
  <div>
    <h1>Hello, {{ welcomeMessage }}</h1>
    <MenuBar />
    <CreateEvent />
  </div>
</template>

<script>
import axios from "axios";
import MenuBar from "../components/MenuBar.vue";
import CreateEvent from "../components/CreateEvent.vue";

export default {
  name: "MainView",
  components: {
    MenuBar,
    CreateEvent,
  },

  MenuBardata() {
    return {
      welcomeMessage: "",
    };
  },
  mounted() {
    axios
      .get("/api/users/welcome")
      .then((response) => {
        this.welcomeMessage = response.data;
        console.log(this.welcomeMessage);
      })
      .catch((error) => {
        console.error(error);
      });
  },
};
</script>

<style scoped>
</style>