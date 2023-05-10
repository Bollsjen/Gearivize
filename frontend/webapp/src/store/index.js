import Vue from 'vue'
import Vuex from 'vuex'

import {authService} from "@/services/authService";

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    isAuthenticated: {},
    isChecking: true,
  },
  getters: {
  },
  mutations: {
    setAuthenticated(state, isAuthenticated){
      state.isAuthenticated = isAuthenticated
    },
    setIsChecking(state, isChecking){
      state.isChecking = isChecking
    }
  },
  actions: {
    checkAuthentication({commit}){
      commit('setIsChecking', true)
      authService.checkAuthenticationcation()
          .then(result => {commit('setAuthenticated', result.data); commit('setIsChecking', false)})
          .catch(error => {commit('setAuthenticated', {responsible: false, superUser: false}); commit('setIsChecking', false)})
    }
  },
  modules: {
  }
})
