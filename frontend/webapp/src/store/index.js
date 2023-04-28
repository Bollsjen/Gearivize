import Vue from 'vue'
import Vuex from 'vuex'

import {storeService} from "@/services/storeService";

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    isAuthenticated: {},
  },
  getters: {
  },
  mutations: {
    setAuthenticated(state, isAuthenticated){
      state.isAuthenticated = isAuthenticated
    }
  },
  actions: {
    async checkAuthentication({commit}){
      storeService.checkAuthenticationcation()
          .then(result => commit('setAuthenticated', result.data))
          .catch(error => commit('setAuthenticated', {}))
    }
  },
  modules: {
  }
})
