<template>
  <div
    class="m-2 bg-white text-dark col-3"
    @click="$router.push({ name: 'KeepDeets', params: {keepId: keep.id}})"
  >
    <img class="img-fluid" :src="keep.img" />
    <div>
      <h4>{{keep.name}}</h4>
      <div>{{keep.description}}</div>
      <div>Kept:{{keep.keeps}}</div>
      <div>Views:{{keep.views}}</div>
    </div>
    <button
      v-if="keep.vaultKeepId"
      class="btn btn-danger"
      @click="removeFromMyVault"
    >Remove from Vault</button>
  </div>
</template>
<script>
export default {
  name: "keepComponent" /*  */,
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
  props: ["keep", "vaultId"],
};
</script>

<style scoped>
</style>