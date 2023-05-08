import Vue from 'vue'
import App from './App.vue'
import './registerServiceWorker'
import router from './router'
import store from './store'
import '@fortawesome/fontawesome-free/css/all.min.css';
import { BootstrapVue, IconsPlugin } from 'bootstrap-vue'
import Vuelidate from "vuelidate";
import '@/assets/main.scss'
import VueApexCharts from 'vue-apexcharts'


Vue.use(VueApexCharts)

Vue.component('apexchart', VueApexCharts)
// Make BootstrapVue available throughout your project
Vue.use(BootstrapVue)
// Optionally install the BootstrapVue icon components plugin
Vue.use(IconsPlugin)

Vue.use(Vuelidate)

Vue.config.productionTip = false

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
