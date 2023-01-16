<template>
  <div class="content">
    <div class="welcometext">
    <h1>LeCrevent</h1>
    <h2>Sportolnál de nincs kivel ?<br> Csatlakozz hozzánk és mi segítünk.</h2>
    
    <div class="gombok">
      <v-layout row>
        <v-flex xs12 sm6>
      <v-btn @click="loginDialog = !loginDialog"
              elevation="2"
              rounded
              class="link1"
          >Bejelentkezés</v-btn>
        </v-flex>
        <v-flex xs12 sm6>
          <v-btn @click="registerDialog = !registerDialog"
              elevation="2"
              rounded
              class="link2"
          >Regisztráció</v-btn>
        </v-flex>
          </v-layout>
    </div>
    </div>
    <div class="mainphotho">
      <v-img class= "logo" src="../assets/landing.jpg"></v-img>
    </div>
    <v-dialog v-model= "registerDialog"
    max-width="40%">
    <user-register />
  </v-dialog>
  <v-dialog v-model= "loginDialog"
    max-width="40%">
    <user-login />
  </v-dialog>
  </div>
</template>

<script>
import userRegister from "../components/userRegister.vue"

export default {
  name: 'HelloWorld',
  components: {
    userRegister
  },
  data: () => ({
    registerDialog: false,
    loginDialog: false,
  }),

  
}
</script>

<style scoped>
.welcometext{
  margin-top: -6%;
  width: 100%;
  margin-right: 10%;
}
.gombok{
  margin-top : 14%;
}
.v-btn.link1, .v-btn.link2{
  text-decoration: none;
  padding: 2rem;
  border-radius: 1.1rem;
  font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
  font-size: 1.5rem;
  
}
.v-btn.link1{
    margin-right: 2rem;
    background-color: white !important;
    color: #3E1E68;
}
.v-btn.link2{
    background-color: yellow;
    color: #3E1E68;
    
}
.v-btn:hover {
  transform: translateY(-4px);
  box-shadow: 0px 15px 20px rgba(0, 0, 0, 0.2);
}

.content{
  display: flex;
  justify-content: space-between;
  margin: 0 auto;
  width: 82%;
}
.content .mainphoto{
  width : 30%;
}
.logo {
  border-radius: 20% 50% 50% 30% / 30% 30% 70% 70%;
  margin-top: -5%;
  width: 37%;
  opacity: 0.45;
}
h1, h2{
  color:white;
  text-align: left;
  font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
  margin-left: 4%;
}
h1{
  font-size: 6.625rem;
  font-weight: bold;
  padding-right: 4.5rem;
}
h2{
  font-size: 1.5rem;
  font-weight: 100;
}
</style>