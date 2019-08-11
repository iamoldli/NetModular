import Vue from 'vue'
import VueRouter from 'vue-router'
import SkinsRoutes from './routes'
import 'nprogress/nprogress.css'
import NProgress from 'nprogress'
import token from './token'

// 错误路由
const errorRoutes = ['error403', 'error404']

Vue.use(VueRouter)

// 进度条初始值
NProgress.configure({ minimum: 0.2 })

/** 路由扩展 */
export default (routerConfig, store, system) => {
  routerConfig.routes = routerConfig.routes || []

  routerConfig.routes = routerConfig.routes.concat(SkinsRoutes)

  const router = new VueRouter({ routes: routerConfig.routes })

  // 路由过滤器
  router.beforeEach((to, from, next) => {
    // 开始进度条
    NProgress.start()
    // 默认页
    const homeRoute = system.home
    // 如果访问的时 / 或者 /default，则跳转到首页
    if (homeRoute && (to.path === '/' || to.path === '/default')) {
      if (homeRoute.startsWith('http://') || homeRoute.startsWith('https://')) {
        next({ name: 'iframe', params: { url: homeRoute, tn_: '首页' } })
      } else {
        next(homeRoute)
      }

      // 关闭进度条
      NProgress.done()
      return
    }

    // 验证是否已登录，根据本地是否存在token判断
    const _token = token.get()
    if (!_token && to.name !== 'login') {
      next({ name: 'login', query: { redirect: to.fullPath } })
    } else {
      if (to.name === 'login') {
        next()
        // 关闭进度条
        NProgress.done()
      } else {
        // 加载账户信息，内部会做是否已加载判断
        store
          .dispatch('app/account/init', null, {
            root: true
          })
          .then(() => {
            // 错误页
            if (errorRoutes.includes(to.name)) {
              next()
              // 关闭进度条
              NProgress.done()
            } else if (
              !store.state.app.system.permissionValidate ||
              store.getters['app/account/routes'].includes(to.name) ||
              to.path === homeRoute ||
              to.name === 'iframe'
            ) {
              // 打开页面
              store.dispatch('app/page/open', to, { root: true }).then(() => {
                next()

                // 关闭进度条
                NProgress.done()
              })
            } else {
              next({ name: 'error403' })
            }
          })
      }
    }
  })
  // 自定义事件
  if (routerConfig.before) {
    routerConfig.before({ router, store })
  }

  // 保存路由实例，以便在模块中使用
  routerConfig.$router = router
  return router
}
