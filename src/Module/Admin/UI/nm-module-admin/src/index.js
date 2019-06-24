import http from './extensions/http'
import routerConfig from './router/'
import store from './store/'
import Skins from 'nm-lib-skins'
import systemApi from './api/system'
import api from './api/account'
import admin from './module'
import configApi from './api/config'

// 要注册的全局组件列表
let globalComponents = []

// 回调方法列表
let callbacks = []

// 模块列表
let modules = [admin]

// 设置模块状态，默认导入admin模块
const storeConfig = {
  modules: { module: { namespaced: true, modules: { admin: store } } }
}

/**
 * @description 注入路由信息
 * @param {Object} moduleInfo 模块信息
 */
const injectRoutes = moduleInfo => {
  if (moduleInfo.routes) {
    routerConfig.routes = routerConfig.routes.concat(moduleInfo.routes)
  }
}

/**
 * @description 注入状态信息
 * @param {Object} moduleInfo 模块信息
 */
const injectStore = moduleInfo => {
  if (moduleInfo.store) {
    storeConfig.modules.module.modules[moduleInfo.module.code] =
      moduleInfo.store
  }
}

/**
 * @description 注入回调方法
 * @param {Object} moduleInfo 模块信息
 */
const injectCallback = moduleInfo => {
  if (moduleInfo.callback) {
    callbacks.push(moduleInfo.callback)
  }
}
/**
 * @description 注入模块
 */
const injectModule = () => {
  modules.forEach(m => {
    injectRoutes(m)
    injectStore(m)
    injectCallback(m)

    // 添加全局组件
    if (m.components) {
      globalComponents = globalComponents.concat(m.components)
    }
  })
}

/**
 * @description 获取系统信息
 */
const getSystem = async () => {
  // 获取系统信息
  const system = await systemApi.getConfig()

  // 模块列表
  system.modules = modules

  // 退出方法
  system.logout = () => {
    api.logout()
    routerConfig.$router.push({ name: 'Login' })
  }
  // 查询登陆信息方法
  system.getLoginInfo = api.getLoginInfo
  // 修改密码方法
  system.updatePassword = api.updatePassword

  return system
}

export default {
  /**
   * @description 添加模块
   * @param {Object} moduleInfo 模块信息
   */
  addModule(moduleInfo) {
    if (moduleInfo) {
      modules.push(moduleInfo)
    }
  },
  /**
   * @description 启动
   */
  async start(config) {
    // 接口请求地址
    http(config.baseUrl)

    // 加载本地token
    callbacks.push(({ store, Vue }) => {
      store.dispatch('module/admin/token/load', null, { root: true })

      // 注册获取配置项的方法为全局属性
      Vue.prototype.$config = configApi.getValue
    })

    // 注入模块
    injectModule()

    const system = await getSystem()

    // 设置个时间，防止等待页面闪烁
    setTimeout(() => {
      Skins.use({
        system,
        routerConfig,
        storeConfig,
        globalComponents,
        callbacks
      })
    }, 1000)
  }
}
