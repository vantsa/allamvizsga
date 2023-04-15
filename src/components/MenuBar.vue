<template>
  <v-container fluid>
    <v-app-bar app color="#3E1E68" dark>
      <v-img src="../assets/logo.png" max-width="10%" v-on:click="goTo('/feed')" class="logo"></v-img>
      <v-spacer></v-spacer>
      <div class="mobile-menu">
        <v-btn text to="/feed">Főoldal</v-btn>
        <v-btn text to="/events">Eseményeim</v-btn>
        <v-menu offset-y>
          <template v-slot:activator="{ on }">
            <v-avatar color="yellow" size="44" v-on="on">
              <span class="text-h5">{{ initials }}</span>
            </v-avatar>
          </template>
        <v-list>
          <v-list-item v-on:click="goTo('/profile')">
          <v-list-item-icon>
              <v-icon>mdi-account-circle</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Profil</v-list-item-title>
          </v-list-item>
          <v-list-item v-if="isAdmin" v-on:click="goTo('/admin')">
          <v-list-item-icon>
              <v-icon>mdi-shield-account</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Admin</v-list-item-title>
          </v-list-item>
          <v-list-item v-on:click="logOut">
            <v-list-item-icon>
              <v-icon>mdi-logout</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Kijelentkezés</v-list-item-title>
          </v-list-item>
        </v-list>
        </v-menu>
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
      username: "",
      isAdmin: false,
    };
  },
  methods: {
    logOut() {
      localStorage.removeItem("jwtToken");
      this.$router.push("/");
    },
    goTo(url) {
      this.$router.push(url);
    },
  },
  mounted() {
    const token = localStorage.getItem("jwtToken");
    if (!token) {
      console.error("No token found");
      return;
    }
    const user = jwt_decode(token);
    this.initials = `${user.firstName.charAt(0)}${user.lastName.charAt(0)}`;
    this.username = user.username;
    if(this.username == "admin")
    {
      this.isAdmin = true;
    }
  },
};
</script>

<style scoped>
*{
  font-family: 'Baloo-Regular';
}
.mobile-menu {
  margin: 0 2.5rem;
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
.text-h5{
  color: black;
}
.theme--light.v-list {
  background-color: #3e1e68;
}
.v-list-item .v-list-item__title
{
  color: white;
}
.theme--light.v-icon  {
  color: white;
}
.v-application .text-h5:hover{
  cursor: pointer;
}
.logo:hover{
  cursor:pointer;
}
.logo{
  margin-left: 3%;
}
.v-avatar{
  margin-left: 5px;
}
</style>