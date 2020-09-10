<template>
  <div class="container-fluid">
    <div class="row justify-content-center">
      <div class="col-12 text-center">
        <h5>Viewed: {{ keep.views }}</h5>
        <h5>Kept: {{ keep.keeps }}</h5>

        <div class="dropdown" v-if="$auth.isAuthenticated">
          <button
            class="btn btn-outline-primary dropdown-toggle interact-buttons"
            type="button"
            id="dropdownMenuButton"
            data-toggle="dropdown"
            aria-haspopup="true"
            aria-expanded="false"
          >Add to a Vault</button>
          <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
            <a
              class="dropdown-item"
              v-for="vault in vaults"
              :key="vault.id"
              href="#"
              @click="addKeepToVault(vault.id)"
            >{{ vault.name }}</a>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import vault from "../components/VaultComponent";
export default {
  name: "keepDeets",
  beforeRouteLeave(to, from, next) {
    this.$store.state.KeepsStore.activeKeep = {};
    next();
  },
  mounted() {
    this.$store.dispatch("getKeepById", this.$route.params.id);
    this.$store.dispatch("getMyVaults");
  },
  methods: {
    addKeepToVault(vaultId) {
      let newVaultKeep = { keepId: this.keep.id, vaultId: vaultId };
      this.$store.dispatch("addKeepToVault", {
        keep: this.keep,
        vaultKeep: newVaultKeep,
      });
    },
    deleteKeep() {
      this.$store.dispatch("deleteKeep", this.$route.params.id);
    },
  },
  computed: {
    keep() {
      return this.$store.state.KeepsStore.activeKeep;
    },
    vaults() {
      return this.$store.state.VaultsStore.myVaults;
    },
    isCreator() {
      return this.$auth.user.sub == this.keep.userId;
    },
  },
  components: {
    vault,
  },
};
</script>


<style scoped>
</style>