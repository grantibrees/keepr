import Vue from "vue";
import router from "../router";
import { api } from "./AxiosService";

export const KeepsStore = {
  state: {
    myPrivateKeeps: [],
    myPublicKeeps: [],
    allKeeps: [],
    activeKeep: {},
  },

  mutations: {
    setMyPrivateKeeps(state, keeps) {
      state.myPrivateKeeps = keeps;
    },
    setMyPublicKeeps(state, keeps) {
      state.myPublicKeeps = keeps;
    },
    setAllKeeps(state, keeps) {
      state.allKeeps = keeps;
    },
    setActiveKeep(state, keep) {
      state.activeKeep = keep;
    },
    addNewPublicKeep(state, keep) {
      state.myPublicKeeps.push(keep);
    },
    setKeepNumbers(state, keep) {
      state.activeKeep = keep;
    },
  },

  actions: {
    async getMyPublicKeeps({ commit, dispatch }) {
      try {
        let res = await api.get("keeps");
        console.log(res.data);
        commit("setMyPublicKeeps", res.data);
      } catch (error) {
        console.error(error);
      }
    },
    async addKeepToVault({ commit, dispatch }, keepData) {
      await api.post("vaultkeeps", keepData.vaultKeep);
      dispatch("incrementKeepKeptCount", keepData.keep);
    },
    async getKeepById({ commit, dispatch }, keepId) {
      try {
        let res = await api.get("keeps/" + keepId);
        commit("setKeepNumbers", res.data);
        dispatch("increaseKeepViews", res.data);
      } catch (error) {
        console.error(error);
      }
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
        keep.keeps++;
        let res = await api.put("keeps/" + keep.id, keep);
        commit("setKeepDetails", res.data);
      } catch (error) {
        console.error(error);
      }
    },
    async increaseKeepShares({ commit, dispatch }, keep) {
      try {
        keep.shares++;
        let res = await api.put("keeps/" + keep.id, keep);
        commit("setKeepDetails", res.data);
      } catch (error) {
        console.error(error);
      }
    },
    // async getMyKeeps({ commit, dispatch }) {
    //   try {
    //     let res = await api.get("keeps/my-keeps");
    //     commit("setMyKeeps", res.data);
    //   } catch (error) {
    //     console.error(error);
    //   }
    // },
    async addKeep({ commit, dispatch }, newKeep) {
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
  },
};
