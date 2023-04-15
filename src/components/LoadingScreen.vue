<template>
  <div class="loading-screen">
    <v-progress-circular
      indeterminate
      color="white"
    ></v-progress-circular>
  </div>
</template>
<script>
export default {
  name: "LoadingScreen",
};
</script>


<style scoped>
.loading-screen {
  position: fixed;
  z-index: 999;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: #3E1E68;
  display: flex;
  justify-content: center;
  align-items: center;
}
</style>