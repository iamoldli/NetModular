import token from '../api/token'
import api from '../api/account'
import routes from './routes'

/** 路由实例 */
const $router = null

const before = ({ router, store }) => {
  router.beforeEach((to, from, next) => {
    const t = token.get()
    if (!t && to.path !== '/login') {
      next('/login')
    } else {
      const account = api.get()
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
