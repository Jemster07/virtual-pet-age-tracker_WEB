<template>
  <div class="home">
    <h1>Home</h1>
    <p>This is the home page.</p>
  </div>
</template>

<script>
export default {
  created() {
    this.$store.commit("DISPLAY_LOGIN_LINK", true);
  }
};
</script>
