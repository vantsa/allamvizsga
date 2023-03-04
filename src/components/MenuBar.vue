<template>
<v-container fluid>
  <v-app-bar app color="#3E1E68" dark>
    <h1>LeCrevent</h1>
    <v-spacer></v-spacer>
    <div class="mobile-menu">
    <v-btn text to="/feed">Főoldal</v-btn>
    <v-btn text to="/profile">Profil</v-btn>
    <v-btn text to="/sajat-esemenyek">Eseményeim</v-btn>
    <v-btn class="logoutbtn" text to="/"><v-icon>mdi-logout</v-icon></v-btn>
    </div>
  </v-app-bar>
  </v-container>
</template>

<script>
export default {
  name: 'MenuBar',
}
</script>

<style scoped>
.mobile-menu {
    margin: 0 2.5rem;
}
h1{
    margin: 0 5rem;
}
.v-btn {
  background-color: #3E1E68;
  color: #FFFFFF;
  border-color: #FFFFFF;
}
.v-btn:hover {
  color: #3E1E68;
  background-color: #FFFFFF;
  transition: background-color 0.4s ease-in-out;
}
.logoutbtn{
    margin-left: 2rem;
}
</style>