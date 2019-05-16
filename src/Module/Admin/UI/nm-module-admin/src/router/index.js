import token from '../extensions/token'
import routes from './routes'
console.log(routes)
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
        // 加载账户信息，内部会做是否已加载判断
        store.dispatch('app/account/init', null, {
          root: true
        })
        next()
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
