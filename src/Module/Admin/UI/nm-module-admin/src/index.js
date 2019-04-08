import '@babel/polyfill'
import http from './extensions/http'
import routerConfig from './router/'
import store from './store/'
import Skins from 'nm-lib-skins'
import configApi from './api/config'
import api from './api/account'

// 要注册的全局组件列表
let globalComponents = []
// 模块列表
let modules = []
// 设置状态，默认导入admin模块
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
    storeConfig.modules.module.modules[moduleInfo.name] = moduleInfo.store
  }
}

/**
 * @description 注入模块
 */
const injectModule = () => {
  modules.forEach(m => {
    injectRoutes(m)
    injectStore(m)

    // 添加全局组件
    if (m.components) {
      globalComponents = globalComponents.concat(m.components)
    }
  })
}

export default {
  /**
     * @description 添加模块
     * @param {Object} moduleInfo 模块信息
     */
  addModule (moduleInfo) {
    if (moduleInfo) {
      modules.push(moduleInfo)
    }
  },
  /**
     * @description 启动
     */
  async start (config) {
    // 接口请求地址
    http(config.baseUrl)

    // 注入模块
    injectModule()

    // 获取系统信息
    const system = await configApi.getSystemConfig()

    // 退出方法
    system.logout = () => {
      api.logout()
      routerConfig.$router.push({ name: 'Login' })
    }
    // 修改密码
    system.updatePassword = api.updatePassword

    // 设置个时间，防止等待页面闪烁
    setTimeout(() => {
      Skins.use({ system, routerConfig, storeConfig, globalComponents })
    }, 1000)
  }
}
