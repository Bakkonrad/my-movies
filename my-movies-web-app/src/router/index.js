import { createRouter, createWebHistory } from 'vue-router'
import MovieTable from '../components/MovieTable.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: MovieTable
    }
  ]
})

export default router
