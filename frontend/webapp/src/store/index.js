import Vue from 'vue'
import Vuex from 'vuex'

import {authService} from "@/services/authService";

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
      authService.checkAuthenticationcation()
          .then(result => {commit('setAuthenticated', result.data);})
          .catch(error => commit('setAuthenticated', {}))
    }
  },
  modules: {
  }
})
