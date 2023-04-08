<template>
  <div class="loading-screen">
    <v-img src="../assets/1483.gif" height="90px" width="90px"></v-img>
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
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
  background-color: #3E1E68;
  z-index: 9999;
}
</style>