<template>
  <div class="container-fluid">
    <div class="row justify-content-center">
      <div class="col-12 text-center">
        <h5>Viewed: {{ keep.views }}</h5>
        <h5>Kept: {{ keep.keeps }}</h5>
        <img :src="keep.img" />
        <h4>{{keep.name}}</h4>
        <div>{{keep.description}}</div>
      </div>

      <div class="dropdown">
        <button
          class="btn btn-secondary dropdown-toggle"
          type="button"
          id="dropdownMenuButton"
          data-toggle="dropdown"
          aria-haspopup="true"
          aria-expanded="false"
        >Add to a vault</button>
        <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
          <a v-for="vault in vaults" :key="vault.id" href="#" class="dropdown-item">
            <p @click="addKeepToVault(vault.id)">{{vault.name}}</p>
          </a>
        </div>
      </div>
      <div v-if="isOwner == true">
        <button class="btn btn-danger btn-sm" @click="deleteKeep">Delete Keep Forever</button>
      </div>
    </div>
  </div>
</template>


<script>
import vault from "../components/VaultComponent";
import { onAuth } from "@bcwdev/auth0-vue";
export default {
  name: "keepDeets",
  beforeDestroy() {
    this.$store.state.KeepsStore.activeKeep = {};
  },
  async mounted() {
    await onAuth();
    if (this.$auth.isAuthenticated) {
      this.$store.dispatch("getKeepById", this.$route.params.keepId);
    }
  },
  data() {
    return {
      isOwner: false,
    };
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
      this.$store.dispatch("deleteKeep", this.$route.params.keepId);
    },
  },
  computed: {
    keep() {
      return this.$store.state.KeepsStore.activeKeep;
    },
    vaults() {
      return this.$store.state.VaultsStore.myVaults;
    },
    owner() {
      if (this.$auth.user.sub == this.keep.userId) {
        return (this.isOwner = true);
      }
    },
  },
  components: {
    vault,
  },
};
</script>


<style scoped>
</style>