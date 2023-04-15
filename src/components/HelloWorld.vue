<template>
  <div class="content">
    <div class="welcometext">
     <div>
        <transition enter-active-class="animated slideInRight">
            <h1 v-if="showText" class="animated">LeCrevent</h1>
        </transition>
    </div>
    <div>
        <transition enter-active-class="animated slideInLeft">
            <h2 v-if="showText2" class="animated">Sportolnál, de nincs kivel?<br> Csatlakozz és legyél te is a csapat tagja!</h2>
        </transition>
    </div>
   
    <div class="gombok">
      <v-layout row>
        <v-flex xs12 sm6 class="justify-center align-center">
          <transition enter-active-class="animated slideInDown">
            <v-btn @click="loginDialog = !loginDialog"
              elevation="2"
              rounded
              class="link1 animated"
              v-if="showButton"
          >Bejelentkezés</v-btn>
          </transition>
        </v-flex>
        <v-flex xs12 sm6 class="justify-center align-center">
          <transition enter-active-class="animated slideInDown">
          <v-btn @click="registerDialog = !registerDialog"
              elevation="2"
              rounded
              class="link2 animated"
              v-if="showButton"
          >Regisztráció</v-btn>
          </transition>
        </v-flex>
          </v-layout>
    </div>
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
import userLogin from "../components/userLogin.vue"

export default {
  name: 'TesztEst',
  components: {
    userRegister,
    userLogin,
  },
  data: () => ({
    registerDialog: false,
    loginDialog: false,
    showText: false,
    showText2: false,
    showButton: false,
  }),
  mounted(){
    this.showText = true,
    setTimeout(() => {
          this.showText2 = true
        }, 1000),
    setTimeout(() => {
          this.showButton = true
        }, 1500);
  }

  
}
</script>

<style scoped>
*{
  font-family: 'Baloo-Regular';
}
.welcometext{
  margin: 0 auto;
  background: rgba(86,41,144, 0.375);
  box-shadow: 0 0.75rem 2rem 0 rgb(0 0 0 / 10%);
  padding: 2rem 6rem 8rem 6rem;
  border-radius: 2rem;
  margin-top: -6%;
}
.gombok{
  margin: 0 auto;
  text-align: center;
  display: flex;
}
.v-btn.link1, .v-btn.link2{
  text-decoration: none;
  padding: 2rem;
  border-radius: 1.1rem;
  font-size: 1.8rem;
  
}
.v-btn.link1{
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
  overflow-wrap: break-word;
}
h1, h2{
  color:white;
  text-align: left;
}
h1{
  font-size: 9.5rem;
  font-weight: bold;
}
h2{
  font-size: 2rem;
  font-weight: 100;
  text-align: center;
  margin-bottom : 15%;
}
.slideInRight {
        animation: slideInRight 1s ease;
    }
    @keyframes slideInRight {
        0% {
            transform: translateX(100%);
        }
        100% {
            transform: translateX(0);
        }
}
.slideInLeft {
        animation: slideInLeft 1s ease;
    }
    @keyframes slideInLeft {
        0% {
            transform: translateX(-100%);
        }
        100% {
            transform: translateX(0);
        }
}
.slideInDown {
        animation: slideInDown 1s ease;
}
@keyframes slideInDown {
        from {
            transform: translateY(200%);
        }
        to {
            transform: translateY(0);
        }
}
@media screen and (max-width:1150px) {
.v-btn.link1, .v-btn.link2{
  font-size: 1.2rem;
}
h1{
  font-size: 6.5rem;
  text-align: center;
}
.content {
  margin-top: 15%;
}
}

@media screen and (max-width:800px) {
h1{
  font-size: 5rem;
  text-align: center;
}
h2{
  font-size: 1.6rem;
}
.v-btn.link1, .v-btn.link2{
  font-size: 1.1rem;
  margin-bottom : 5%;
}
.gombok{
  flex-direction: column;
  flex-wrap: wrap;
}
.welcometext{
  padding: 2rem 4rem 10rem 4rem;
}
}
@media screen and (max-width:700px){
.row{
  margin: -35px;
}
}
@media screen and (max-width:575px){
h1{
  font-size: 4rem;
  text-align: center;
}
h2{
  font-size: 1.25rem;
  margin-top: 10%;
  margin-bottom: 10%;
}
.v-btn.link2{
  font-size: 1rem;
  margin-top: 5%;
}
.v-btn.link1{
  font-size: 1rem;
  margin-top: 10%;
}
.welcometext{
  padding: 2rem 4rem 6rem 4rem;
}
}
@media screen and (max-width:500px) {
h1{
  font-size: 3rem;
  text-align: center;
}
h2{
  font-size: 1rem;
}
.welcometext{
  padding: 2rem 2.5rem 3rem 2.5rem;
}
.v-btn.link1{
  margin-top: 10%;
}
.v-btn.link2{
  margin-top: 5%;
}
}
@media screen and (max-width:320px){
  .welcometext{
    padding: 1.5rem 1rem 2rem 1rem;
}
.v-btn.link1{
  margin-top: 25%;
}
.v-btn.link2{
  margin-top: 5%;
}
}
</style>