import NetModularUI from 'netmodular-ui'
import './api'
import module from './module'
import routes from './routes'
import store from './store'
import components from './components'
import mixins from './mixins'
import NetmodularSkinsClassics from 'netmodular-skins-classics'

const admin = {
  module,
  routes,
  store,
  components
}

// 模块列表
let modules = [admin]

export default {
  /**
   * @description 注册模块
   * @param {Object} module 模块信息
   */
  registerModule(module) {
    if (module) modules.push(module)
  },
  /**
   * @description 注册皮肤
   * @param {object} skin 皮肤
   */
  registerSkin(skin) {
    if (skin) NetModularUI.useSkin(skin)
  },
  /**
   * @description 启动
   */
  async start(config) {
    // 设置接口信息
    NetModularUI.configApi(config)
    // 使用皮肤
    NetModularUI.registerSkin(NetmodularSkinsClassics)

    // 获取UI配置信息
    const UIConfig = await $api.admin.config.getUI()

    // 方法
    let actions = {
      //身份认证相关方法
      ...$api.admin.auth,
      // 修改密码方法
      updatePassword: $api.admin.account.updatePassword,
      // 皮肤修改方法
      saveSkin: $api.admin.account.skinUpdate
    }

    // 设置账户类型
    if (config.accountTypes) {
      UIConfig.login.accountTypes = config.accountTypes
    }

    window.loaded = true
    const t = setInterval(() => {
      if (window.loadProgress > 98) {
        clearInterval(t)
        NetModularUI.use({ config: UIConfig, modules, actions })
      }
    }, 20)
  }
}

export { mixins }
