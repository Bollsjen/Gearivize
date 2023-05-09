import Vue from 'vue'
import VueRouter from 'vue-router'
import store from '@/store'
import DashboardFrontPage from "@/views/DashboardFrontPage.vue";
import InstrumentsPage from "@/views/InstrumentsPage.vue";
import TemplatesPage from "@/views/TemplatesPage.vue";
import SiginPage from "@/views/SiginPage.vue";
import SettingsPage from "@/views/SettingsPage.vue";
import SuperUserPage from "@/views/SuperUserPage.vue";

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
  },
  {
    path: '/super-user',
    name: 'Super user',
    component: SuperUserPage
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

router.beforeEach((to, from, next) => {
  store.dispatch('checkAuthentication')
      .then(() => next())
      .catch(() => next())
})

export default router
