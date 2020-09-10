import Vue from "vue";
import router from "../router";
import { api } from "./AxiosService";

export const KeepsStore = {
  state: {
    myKeeps: [],
    allKeeps: [],
    activeKeep: {},
  },

  mutations: {
    setMyKeeps(state, keeps) {
      state.myKeeps = keeps;
    },
    setAllKeeps(state, keeps) {
      state.allKeeps = keeps;
    },
    setActiveKeep(state, keep) {
      state.activeKeep = keep;
    },
    addNewKeep(state, keep) {
      state.myKeeps.push(keep);
    },
    setKeepNumbers(state, keep) {
      state.activeKeep = keep;
    },
  },

  actions: {
    async getMyKeeps({ commit, dispatch }) {
      try {
        let res = await api.get("keeps");
        console.log(res.data);
        commit("setMyKeeps", res.data);
      } catch (error) {
        console.error(error);
      }
    },
    async getAllKeeps({ commit, dispatch }) {
      try {
        let res = await api.get("keeps");
        console.log(res.data);
        commit("setAllKeeps", res.data);
      } catch (error) {
        console.error(error);
      }
    },
    async addKeepToVault({ commit, dispatch }, keepData) {
      await api.post("vaultkeeps", keepData.vaultKeep);
      dispatch("incrementKeepKeptCount", keepData.keep);
    },

    async incrementKeepViewCount({ commit, dispatch }, keep) {
      try {
        keep.viewCount += 1;
        let res = await api.put("keeps/" + keep.id, keep);
        commit("setKeepNumbers", res.data);
      } catch (error) {
        console.error(error);
      }
    },
    async incrementKeepKeptCount({ commit, dispatch }, keep) {
      try {
        keep.keeps += 1;
        let res = await api.put("keeps/" + keep.id, keep);
        commit("setKeepNumbers", res.data);
      } catch (error) {
        console.error(error);
      }
    },
    async addNewKeep({ commit, dispatch }, newKeep) {
      try {
        let res = await api.post("keeps", newKeep);
        commit("addNewKeep", res.data);
      } catch (error) {
        console.error(error);
      }
    },
    async deleteKeep({ commit, dispatch }, keepId) {
      try {
        await api.delete("keeps/" + keepId);
        router.push({ name: "keeps" });
      } catch (error) {
        console.error(error);
      }
    },

    async getKeepById({ commit, dispatch }, keepId) {
      try {
        let res = await api.get("keeps/" + keepId);
        commit("setKeepNumbers", res.data);
        dispatch("incrementKeepViewCount", res.data);
      } catch (error) {
        console.error(error);
      }
    },
  },
};
