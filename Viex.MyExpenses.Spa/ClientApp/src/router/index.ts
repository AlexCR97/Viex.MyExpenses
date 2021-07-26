import Vue from 'vue'
import VueRouter, { RouteConfig } from 'vue-router'
import Login from '@/views/Login.vue'
import MainContainer from '@/views/MainContainer.vue'
import Developer from '@/views/Developer/Developer.vue'
import Home from '@/views/Home/Home.vue'
import Transaction from '@/views/Transaction'

Vue.use(VueRouter)

const routes: Array<RouteConfig> = [
  {
    path: '/',
    redirect: '/login',
    component: MainContainer,
    children: [
      {
        path: 'home',
        component: Home,
      },
      {
        path: 'developer',
        component: Developer,
      },
      {
        path: 'transaction/:id?',
        component: Transaction,
      },
    ],
  },
  {
    path: '/login',
    component: Login,
  },
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
