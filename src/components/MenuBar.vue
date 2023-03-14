<template>
  <v-container fluid>
    <v-app-bar app color="#3E1E68" dark>
      <h1>LeCrevent</h1>
      <v-spacer></v-spacer>
      <div class="mobile-menu">
        <v-btn text to="/feed">Főoldal</v-btn>
        <v-btn text to="/profile">Profil</v-btn>
        <v-btn text to="/sajat-esemenyek">Eseményeim</v-btn>
        <v-avatar color="yellow" size="44">
          <span class="text-h5"> {{ initials }} </span>
        </v-avatar>
        <v-btn class="logoutbtn" text to="/"><v-icon>mdi-logout</v-icon></v-btn>
      </div>
    </v-app-bar>
  </v-container>
</template>

<script>
import jwt_decode from "jwt-decode";

export default {
  name: "MenuBar",
  data() {
    return {
      initials: "",
    };
  },
  mounted() {
    const token = localStorage.getItem("jwtToken");
    if (!token) {
      console.error("No token found");
      return;
    }
    const user = jwt_decode(token);
    this.initials = `${user.firstName.charAt(0)}${user.lastName.charAt(0)}`;
  },
};
</script>

<style scoped>
.mobile-menu {
  margin: 0 2.5rem;
}
h1 {
  margin: 0 5rem;
}
.v-btn {
  background-color: #3e1e68;
  color: #ffffff;
  border-color: #ffffff;
}
.v-btn:hover {
  color: #3e1e68;
  background-color: #ffffff;
  transition: background-color 0.4s ease-in-out;
}
.logoutbtn {
  margin-left: 2rem;
}
span {
  color: purple;
  padding: 5px;
  font-weight: bold;
}
</style>