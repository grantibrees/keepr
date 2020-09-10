<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-12 bg-white">
        <h2>{{vault.name}}</h2>
        <h5>{{vault.description}}</h5>
        <div class="row">
          <h1>YOUR VAULTS</h1>
          <button class="btn btn-outline-success" @click="showKeepForm = !showKeepForm">+</button>
        </div>
        <keepForm v-if="showKeepForm" />
        <div class="row" v-show="keeps.length == 0">
          <div>
            <h5>No keeps yet!</h5>
          </div>
        </div>

        <button class="btn btn-outline-danger" @click="deleteKeep">Delete Keep</button>
      </div>
    </div>
    <div class="row">
      <keep v-for="keep in keeps" :key="keep.id" :keep="keep" :vaultId="vault.id" />
    </div>
  </div>
</template>

<script>
import keep from "../components/KeepComponent";
export default {
  name: "VaultDeets",
  // beforeRouteLeave(to, from, next) {
  //   this.$store.state.VaultsStore.activeVault = {};
  //   next();
  // },
  mounted() {
    this.$store.dispatch("getVaultById", this.$route.params.id);
    this.$store.dispatch("getKeepsByVaultId", this.$route.params.id);
  },
  computed: {
    vault() {
      return this.$store.state.VaultsStore.activeVault;
    },
    keeps() {
      return this.$store.state.VaultsStore.vaultKeeps;
    },
  },
  methods: {
    deleteVault() {
      this.$store.dispatch("deleteVault", this.$route.params.id);
    },
  },
  components: {
    keep,
  },
};
</script>

<style>
</style>