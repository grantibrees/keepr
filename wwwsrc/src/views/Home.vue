<template>
  <div class="home container-fluid">
    <div class="row">
      <h1>Welcome to Keepr</h1>
    </div>
    <div class="row">
      <keep v-for="keep in allKeeps" :key="keep.Id" :keep="keep" />
    </div>
  </div>
</template>

<script>
import keep from "../components/KeepComponent.vue";
export default {
  name: "home",
  mounted() {
    this.$store.dispatch("getAllKeeps");
  },
  computed: {
    user() {
      return this.$store.state.user;
    },
    allKeeps() {
      return this.$store.state.KeepsStore.allKeeps;
    },
  },
  methods: {
    logout() {
      this.$store.dispatch("logout");
    },
  },
  components: { keep },
};
</script>
