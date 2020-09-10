<template>
  <div class="m-2" @click="router.push({ name: 'KeepDeets', params: {id: keep.id}})">
    <img :src="keep.img" />
    <div>
      <h4>{{keep.name}}</h4>
      <div>{{keep.description}}</div>
    </div>
  </div>
</template>
<script>
export default {
  name: "keepComponent" /*  */,
  props: ["keep", "vaultId"],
  data() {
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
    removeFromMyVault() {
      let vkRelationshipData = {
        vaultId: this.vaultId,
        vaultKeepId: this.keep.vaultKeepId,
      };
      this.$store.dispatch("deleteVKrelationship", vkRelationshipData);
    },
  },
};
</script>

<style scoped>
</style>