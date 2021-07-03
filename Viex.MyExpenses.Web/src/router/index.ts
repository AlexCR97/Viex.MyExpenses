import Vue from 'vue'
import VueRouter, { RouteConfig } from 'vue-router'
import MainContainer from '@/views/MainContainer.vue'
import Home from '@/views/Home/Home.vue'
import Transaction from '@/views/Transaction'

Vue.use(VueRouter)

const routes: Array<RouteConfig> = [
  {
    path: '/',
    redirect: '/home',
    component: MainContainer,
    children: [
      {
        path: 'home',
        component: Home,
      },
      {
        path: 'transaction/:id?',
        component: Transaction,
      },
    ],
  },
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
