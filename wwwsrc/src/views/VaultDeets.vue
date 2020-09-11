<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-12 bg-white text-dark">
        <h2>{{vault.name}}</h2>
        <h5>{{vault.description}}</h5>
        <button class="btn btn-outline-info" @click="showKeepForm = !showKeepForm">Add a new Keep</button>
        <keepForm v-if="showKeepForm" />
        <div class="row" v-show="keeps.length == 0">
          <div>
            <h5>No keeps yet!</h5>
          </div>
        </div>

        <button class="btn btn-outline-danger" @click="deleteVault">Delete Vault</button>
      </div>
    </div>
    <div class="row">
      <keep v-for="keep in keeps" :key="keep.id" :keep="keep" :vaultId="vault.id" />
    </div>
  </div>
</template>

<script>
import keep from "../components/KeepComponent";
import keepForm from "../components/KeepForm";
export default {
  name: "VaultDeets",
  data() {
    return {
      showKeepForm: false,
    };
  },
  mounted() {
    this.$store.dispatch("getVaultById", this.$route.params.vaultId);
    this.$store.dispatch("getKeepsByVaultId", this.$route.params.vaultId);
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
      this.$store.dispatch("deleteVault", this.$route.params.vaultId);
    },
  },
  components: {
    keep,
    keepForm,
  },
};
</script>

<style>
</style>
