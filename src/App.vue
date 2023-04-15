<template>
  <v-app>
    <v-main>
      <router-view/>
    </v-main>
  </v-app>
</template>

<script>

export default {
  name: 'App',

  data: () => ({
    //
  }),
};
</script>

<style scoped>
@font-face {
  font-family: "Baloo-Regular";
  src: local("Baloo-Regular"),
   url(assets/fonts/Baloo-Regular.ttf) format("truetype");
}
</style>
