<template>
  <div class="home container-fluid">
    <div class="row">
      <h1>Welcome to Keepr</h1>
    </div>
    <div class="row">
      <keep v-for="keep in keeps" :key="keep.Id" :keep="keep" />
    </div>
  </div>
</template>

<script>
import keep from "../components/KeepComponent.vue";
export default {
  name: "home",
  mounted() {
    this.$store.dispatch("getMyKeeps");
  },
  computed: {
    user() {
      return this.$store.state.user;
    },
    publicKeeps() {
      return this.$store.state.KeepsStore.PublicKeeps;
    },
    privateKeeps() {
      return this.$store.state.KeepsStore.PrivateKeeps;
    },
  },
  methods: {
    logout() {
      this.$store.dispatch("logout");
    },
  },
};
</script>
