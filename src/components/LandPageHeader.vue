<template>
<div class="header">
    <div class="social-icons">
            <a href="https://www.instagram.com/vantsaaa/" target="_blank"><v-icon class="pa-2" large>mdi-instagram</v-icon></a>
            <a href="https://www.facebook.com/vantsa.alpar" target="_blank"><v-icon class="pa-2" large>mdi-facebook</v-icon></a>
            <a href="https://www.linkedin.com/in/v%C3%A1ntsa-alp%C3%A1r-a6705a1b3/" target="_blank"><v-icon class="pa-2" large>mdi-linkedin</v-icon></a>
            <a href="https://github.com/vantsa" target="_blank"><v-icon class="pa-2" large>mdi-github</v-icon></a>
    </div>
</div>
    
</template>

<script>
export default {
  name: 'LandPageHeader',
  
}
</script>

<style scoped>
.header{
    display:flex;
    justify-content: flex-end;
    width: 76%;
    margin: 0 auto;
    padding-top: 10px;
    margin-bottom: 10%;
}
a{
    text-decoration: none;
}
.v-icon{
    color:white;
}
.social-icons{
    right: 0;
}
</style>