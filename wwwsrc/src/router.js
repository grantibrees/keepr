import Vue from "vue";
import Router from "vue-router";
// @ts-ignore
import Home from "./views/Home.vue";
// @ts-ignore
import Dashboard from "./views/Dashboard.vue";
// @ts-ignore
import KeepDeets from "./views/KeepDeets.vue"
// @ts-ignore
import VaultDeets from "./views/VaultDeets.vue"
import { authGuard } from "@bcwdev/auth0-vue";

Vue.use(Router);

export default new Router({
  routes: [
    {
      path: "/",
      name: "home",
      component: Home
    },
    {
      path: "/keep/:keepId",
      name: "KeepDeets",
      component: KeepDeets,
    },
    {
      path: "/Dashboard",
      name: "Dashboard",
      component: Dashboard,
      beforeEnter: authGuard
    },
    {
      path: "/vault/:vaultId",
      name: "VaultDeets",
      component: VaultDeets,
      beforeEnter: authGuard,
    },
  ]
});
