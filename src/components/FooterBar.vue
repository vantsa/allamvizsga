<template>
  <div class="footer">
    <div class="container">
      <div class="col-sm-12 col-md-6">
        <h5>Rólam</h5>
        <p>Üdvözöllek a weboldalamon, ez az államvizsgám. A weboldal felépítéséhez fronted terén Vue.js-t illetve vuetify-t használtam
          míg a backend részét C# ASP.NET Core 7-ben készítettem. Az adatbázist MySql-ben hoztam létre.</p>
      </div>
      <div class="col-xs-6 col-md-3">
        <h5>Linkek</h5>
        <ul>
        <li><a href="http://localhost:8080/#/feed">Főoldal</a></li>
        <li><a href="http://localhost:8080/#/sajat-esemenyek">Saját események</a></li>
        <li><a href="http://localhost:8080/#/profile">Profil</a></li>
      </ul>
      </div>
    </div>
    <div class="container">
    <div class="cr">
      <span>All rights reserved by Vántsa Alpár ©2023</span>
    </div>
    <div class="col-xs-6 col-md-3">
      <a href="https://www.instagram.com/vantsaaa/" target="_blank"
        ><v-icon medium>mdi-instagram</v-icon></a
      >
      <a href="https://www.facebook.com/vantsa.alpar" target="_blank"
        ><v-icon medium>mdi-facebook</v-icon></a
      >
      <a
        href="https://www.linkedin.com/in/v%C3%A1ntsa-alp%C3%A1r-a6705a1b3/"
        target="_blank"
        ><v-icon medium>mdi-linkedin</v-icon></a
      >
      <a href="https://github.com/vantsa" target="_blank"
        ><v-icon medium>mdi-github</v-icon></a
      >
    </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "FooterBar",
};
</script>

<style scoped>
*{
  font-family: 'Baloo-Regular';
}
.footer {
  background: #3e1e68;
  margin-top: 5rem;
  min-height: 150px;
}
.container{
  display: flex;
  justify-content: space-between;
  padding: 2rem;
  width: 80%;
  align-items: center;
}
.container:nth-child(even){
  border-top: 0.5px solid white;
  justify-content: space-around;
}
p{
  color:white;
}
span{
  color: white;
  font-size: 1rem;
}
.v-icon{
  color: white;
  font-size: 2rem;
  margin: 0 1rem;
}
.v-icon:nth-child(1){
  margin: 0 1rem 0 0;
}
a{
  text-decoration: none;
  color: white;
}
ul{
    padding-left:0;
  list-style:none
}
li{
  display: block;
  line-height: 2rem;
}
.v-application a {
  color: white;
}
h5
{
  color:#fff;
  font-size:1.1rem;
  text-transform:uppercase;
  margin-top:0.3rem;
  letter-spacing:2px;
  margin-bottom: 0.5rem;
}
@media only screen and (max-width: 1270px){
a{
  margin-left: -1%;
}
}
@media only screen and (max-width: 960px){
.container{
  width: 75%;
  margin-left: 8%;
  align-items: baseline;
}
}
</style>