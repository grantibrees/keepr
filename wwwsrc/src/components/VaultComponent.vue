<template>
  <div class="bg-white" @click="$router.push({name: 'VaultDeets', params: {id: vault.id}})">
    <div class="row">
      <div class="col-12">
        <div class="row">
          <h5 class="card-title">{{vault.name}}</h5>
        </div>
        <div class="row">
          <h5 class="card-text">{{vault.description}}</h5>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "vaultComponent",
  methods: {
    setActiveVault() {
      this.$store.commit("setActiveVault", this.vault);
    },
  },
  props: ["vault"],
};
</script>

<style>
</style>