import Vue from 'vue'
import VueRouter from 'vue-router'
import DashboardFrontPage from "@/views/DashboardFrontPage.vue";
import InstrumentsPage from "@/views/InstrumentsPage.vue";
import InactiveInstrumentsPage from "@/views/InactiveInstrumentsPage.vue";
import TemplatesPage from "@/views/TemplatesPage.vue";
import SiginPage from "@/views/SiginPage.vue";
import SettingsPage from "@/views/SettingsPage.vue";

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Dashboard',
    component: DashboardFrontPage
  },
  {
    path: '/instruments',
    name: 'Instruments',
    component: InstrumentsPage
  },
  {
    path: '/inactive-instruments',
    name: 'Inactive Instruments',
    component: InactiveInstrumentsPage
  },
  {
    path: '/templates',
    name: 'Templates',
    component: TemplatesPage
  },
  {
    path: '/signin',
    name: 'Signin',
    component: SiginPage
  },
  {
    path: '/settings',
    name: 'Settings',
    component: SettingsPage
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
