import Vue from 'vue'
import VueRouter from 'vue-router'
import SkinsRoutes from './routes'
import 'nprogress/nprogress.css'
import NProgress from 'nprogress'

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
    if (homeRoute && (to.path === '/' || to.path === '/default')) {
      if (homeRoute.startsWith('http://') || homeRoute.startsWith('https://')) {
        next({ name: 'Iframe', params: { url: homeRoute, tn_: '首页' } })
      } else {
        next(homeRoute)
      }

      // 关闭进度条
      NProgress.done()
      return
    }
    // 打开页面
    store.dispatch('app/page/open', to, { root: true }).then(() => {
      next()

      // 关闭进度条
      NProgress.done()
    })
  })
  // 自定义事件
  if (routerConfig.before) {
    routerConfig.before({ router, store })
  }

  // 保存路由实例，以便在模块中使用
  routerConfig.$router = router
  return router
}
