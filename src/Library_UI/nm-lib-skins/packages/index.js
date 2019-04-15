import Vue from 'vue'
import lodash from 'lodash'
import ElementUI from 'element-ui'
import 'element-ui/lib/theme-chalk/index.css'
import './styles/app.scss'
import UseRouter from './router/'
import UseStore, { store } from './store/'
import Layout from './layout'
import Icon from 'nm-lib-icon'
import NmComponents from './components'
import NmMixins from './mixins/'
import NmDirective from './directive'

export default {
  /**
   * @description 加载皮肤组件
   */
  use: async ({ system, routerConfig, storeConfig, globalComponents }) => {
    // 将lodash添加到Vue的实例属性
    Vue.prototype.$_ = lodash

    // 全局混入
    NmMixins.global(Vue)

    // 加载饿了么框架
    Vue.use(ElementUI)

    // 加载图标
    Vue.use(Icon)

    // 加载自定义组件
    Vue.use(NmComponents)

    // 注册皮肤组件
    Vue.component('nm-skins', Layout)

    // 注册指令
    Vue.use(NmDirective)

    // 使用状态
    const store = UseStore(storeConfig)

    // 使用路由
    const router = UseRouter(routerConfig, store, system)

    // 加载页面数据
    await store.dispatch('app/system/init', { system, router }, { root: true })

    Vue.config.productionTip = false

    // 注册全局组件
    if (globalComponents) {
      globalComponents.forEach(com => {
        Vue.component(com.name, com.component)
      })
    }

    // 创建根实例
    const vm = new Vue({
      router,
      store,
      render: h => h('nm-skins')
    }).$mount('#app')

    return { router, store, vm }
  }
}

const mixins = NmMixins.components
export { mixins, store }
