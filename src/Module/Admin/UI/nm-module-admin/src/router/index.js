import token from '../extensions/token'
import api from '../api/account'
import routes from './routes'

/** 路由实例 */
var $router = null

// 在干方法中添加路由导航守卫
const before = ({ router, store }) => {
  router.beforeEach((to, from, next) => {
    // 验证是否已登录，根据本地是否存在token判断
    const _token = token.get()
    if (!_token && to.name !== 'Login') {
      next({ name: 'Login', query: { redirect: to.fullPath } })
    } else {
      if (to.name === 'Login') {
        next()
      } else {
        const account = store.state.app.account
        if (!account || account.id === '') {
          // 获取账户登录信息
          api.getLoginInfo().then(account => {
            store.dispatch('app/account/init', account, { root: true })
            next()
          })
        } else {
          next()
        }
      }
    }
  })
}

export { $router, routes, before }

export default {
  $router,
  routes,
  before
}
