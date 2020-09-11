<template>
  <div class="dashboard container-fluid">
    <div class="row">
      <h1>YOUR VAULTS</h1>
      <button
        class="btn btn-outline-success"
        @click="showVaultForm = !showVaultForm"
      >Add a new Vault</button>
      <button class="btn btn-outline-info" @click="showKeepForm = !showKeepForm">Add a new Keep</button>
    </div>
    <vaultForm v-if="showVaultForm" />
    <keepForm v-if="showKeepForm" />
    <div class="row" v-show="vaults.length == 0">
      <div>
        <h5>Please add a vault to save keeps!</h5>
      </div>
    </div>
    <div class="row">
      <vaultComponent v-for="vault in vaults" :key="vault.id" :vault="vault" />
    </div>
  </div>
</template>

<script>
import vaultForm from "../components/VaultForm";
import keepForm from "../components/KeepForm";
import vaultComponent from "../components/VaultComponent";
export default {
  name: "dashboard",
  data() {
    return {
      showVaultForm: false,
      showKeepForm: false,
    };
  },
  mounted() {
    this.$store.dispatch("getMyVaults");
  },
  computed: {
    vaults() {
      return this.$store.state.VaultsStore.myVaults;
    },
  },
  components: {
    vaultForm,
    vaultComponent,
    keepForm,
  },
};
</script>

<style></style>
