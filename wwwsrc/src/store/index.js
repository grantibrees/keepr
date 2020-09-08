import Vue from "vue";
import Vuex from "vuex";
import Axios from "axios";
import router from "../router";
import { api } from "./AxiosService";
import { KeepsStore } from "./KeepsStore";
import { VaultsStore } from "./VaultsStore";



Vue.use(Vuex);


export default new Vuex.Store({
  state: {

  },
  mutations: {},
  actions: {
    setBearer({ }, bearer) {
      api.defaults.headers.authorization = bearer;
    },
    resetBearer() {
      api.defaults.headers.authorization = "";
    }
  },
  modules: {
    KeepsStore,
    VaultsStore
  }
});
