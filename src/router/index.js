import Vue from 'vue'
import VueRouter from 'vue-router'
import HomeView from '../views/HomeView.vue'
import MainView from '../views/MainView.vue'
import ProfileView from '../views/ProfileView.vue'
import EventView from '../views/EventView.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'home',
    component: HomeView,
  },
  {
    path: '/feed',
    name: 'main',
    component: MainView,
  },
  {
    path: '/profile',
    name: 'profile',
    component: ProfileView,
  },
  {
    path: '/events',
    name: 'events',
    component: EventView,
  }
]

const router = new VueRouter({
  routes
})

export default router
