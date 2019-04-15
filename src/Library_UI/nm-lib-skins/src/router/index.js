import token from '../api/token'
import { get } from '../api/account'
import components from './components'

/** 路由实例 */
const $router = null
const routes = [
  {
    path: '/',
    name: 'home',
    component: () => import('../views/home'),
    meta: {
      title: '首页'
    }
  },
  {
    path: '/login',
    name: 'login',
    component: () => import('../views/account/login'),
    meta: {
      frameIn: false,
      cache: false,
      title: '登录'
    }
  },
  ...components
]

const before = ({ router, store }) => {
  router.beforeEach((to, from, next) => {
    const t = token.get()
    if (!t && to.path !== '/login') {
      next('/login')
    } else {
      const account = get()
      store.dispatch('app/account/init', account, { root: true })
      next()
    }
  })
}

export { $router, routes, before }

export default {
  $router,
  routes,
  before
}
