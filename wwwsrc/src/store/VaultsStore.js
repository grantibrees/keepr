import Vue from "vue";
import router from "../router";
import { api } from "./AxiosService";

export const VaultsStore = {
  state: {
    myVaults: [],
    activeVault: {},
    // vaultKeeps: [],
  },

  mutations: {
    setMyVaults(state, vaults) {
      state.myVaults = vaults;
    },
    setActiveVault(state, vault) {
      state.activeVault = vault;
    },
    // setVaultKeeps(state, vaultKeeps) {
    //   state.vaultKeeps = vaultKeeps;
    // },
  },

  actions: {
    async getMyVaults({ commit, dispatch }) {
      try {
        let res = await api.get("vaults");
        commit("setMyVaults", res.data);
      } catch (error) {
        console.error(error);
      }
    },

    async addVault({ commit, dispatch }, newVault) {
      try {
        await api.post("vaults", newVault);
        dispatch("getMyVaults");
      } catch (error) {
        console.error(error);
      }
    },
    async getVaultById({ commit, dispatch }, vaultId) {
      try {
        let res = await api.get("vaults/" + vaultId);
        commit("setActiveVault", res.data);
      } catch (error) {
        console.error(error);
      }
    },
    async getKeepsByVaultId({ commit, dispatch }, vaultId) {
      try {
        let res = await api.get("vaults/" + vaultId + "/keeps");
        commit("setVaultKeeps", res.data);
      } catch (error) {
        console.error(error);
      }
    },
    async deleteVault({ commit, dispatch }, vaultId) {
      try {
        await api.delete("vaults/" + vaultId);
        router.push({ name: "vaults" });
      } catch (error) {
        console.error(error);
      }
    },
    async deleteVaultKeep({ commit, dispatch }, vaultKeepData) {
      try {
        await api.delete("vaultkeeps/" + vaultKeepData.vaultKeepId);
        dispatch("getKeepsByVaultId", vaultKeepData.vaultId);
      } catch (error) {
        console.error(error);
      }
    },
  },
};
