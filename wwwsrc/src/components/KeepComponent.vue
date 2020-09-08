<template>
  <div>Keep thing.</div>
</template>
<script>
export default {
  name: "keepComponent" /*  */,
  props: [
    "keep",
    "vaultId",
  ] /* More like a parameter than anything. Gets passed to Pages. On the page will look like :carData="car" or "carData="{keys: values} */,
  data() {
    /* Data binding. */
    return {};
  },
  mounted() {} /* Runs functions on startup */,
  computed: {
    vaults() {
      return this.$store.state.VaultsStore.myVaults;
    },
  },
  methods: {
    setActiveKeep() {
      this.$store.commit("setActiveKeep", this.keep);
    },
    // removeFromMyVault() {
    //   let vkRelationshipData = {
    //     vaultId: this.vaultId,
    //     vaultKeepId: this.keep.vaultKeepId,
    //   };
    //   this.$store.dispatch("deleteVKrelationship", vkRelationshipData);
    // },
  },
};
</script>

<style scoped>
</style>