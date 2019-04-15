import Vue from 'vue'
import VueRouter from 'vue-router'
import SkinsRoutes from './routes'
import 'nprogress/nprogress.css'
import NProgress from 'nprogress'

Vue.use(VueRouter)

// 进度条初始值
NProgress.configure({ minimum: 0.2 })

/**
 * @description 获取根路径
 * @param {Object} system 系统配置信息
 */
const getRootPage = system => {
  let rootPage = {
    path: '/',
    redirect: '/default'
  }
  const def = system.default
  if (def) {
    if (def.startsWith('http://') || def.startsWith('https://')) {
      rootPage.beforeEnter = () => {
        location.href = def
      }
    } else {
      rootPage.redirect = def
    }
  }
  return rootPage
}

/** 路由扩展 */
export default (routerConfig, store, system) => {
  const rootPage = getRootPage(system)

  routerConfig.routes = routerConfig.routes || []

  routerConfig.routes = routerConfig.routes
    .concat([rootPage])
    .concat(SkinsRoutes)

  const router = new VueRouter({ routes: routerConfig.routes })

  // 路由过滤器
  router.beforeEach((to, from, next) => {
    // 开始进度条
    NProgress.start()

    // 打开页面
    store.dispatch('app/page/open', to, { root: true })

    // 设置标题
    document.title = system.title

    next()

    // 关闭进度条
    NProgress.done()
  })

  // 自定义事件
  if (routerConfig.before) {
    routerConfig.before({ router, store })
  }

  // 保存路由实例，以便在模块中使用
  routerConfig.$router = router
  return router
}
