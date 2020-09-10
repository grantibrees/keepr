<template>
  <div class="vaultForm">
    <form @submit.prevent="addVault">
      <div class="form-group">
        <label for="make">Title</label>
        <input
          v-model="newVault.name"
          type="text"
          name="title"
          class="form-control"
          placeholder="Vault title"
          required
        />
      </div>
      <div class="form-group">
        <label for="model">Description</label>
        <input
          v-model="newVault.description"
          type="text"
          name="description"
          class="form-control"
          placeholder="Vault description"
          required
        />
      </div>

      <button type="submit" class="btn btn-primary">Add</button>
    </form>
  </div>
</template>

<script>
export default {
  name: "vaultForm",
  data() {
    return {
      newVault: {},
    };
  },
  methods: {
    addVault() {
      this.$store.dispatch("addVault", { ...this.newVault });
      this.newVault = {};
    },
  },
};
</script>

<style>
</style>