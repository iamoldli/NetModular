import Vue from 'vue'
import lodash from 'lodash'
import ElementUI from 'element-ui'
import 'element-ui/lib/theme-chalk/index.css'
import './styles/app.scss'
import UseRouter from './router/'
import UseStore, { store } from './store/'
import Layout from './layout'
import Icon from 'nm-lib-icon'
import Components from './components'
import Mixins from './mixins/'
import Directive from './directive'
import dayjs from 'dayjs'
import token from './router/token'

// 附加自定义样式
const appendCustomCss = system => {
  if (system.customCss) {
    var style = document.createElement('style')
    style.type = 'text/css'
    if (style.styleSheet) {
      style.styleSheet.cssText = system.customCss
    } else {
      // w3c浏览器中只要创建文本节点插入到style元素中就行了
      var textNode = document.createTextNode(system.customCss)
      style.appendChild(textNode)
    }
    document.head.appendChild(style)
  }
}

export default {
  /**
   * @description 加载皮肤组件
   */
  use: async ({
    system,
    routerConfig,
    storeConfig,
    globalComponents,
    callbacks
  }) => {
    // 设置标题
    document.title = system.title

    // 将lodash添加到Vue的实例属性
    Vue.prototype.$_ = lodash

    // 全局引用ECharts，如果需要按需引用，请访问http://echarts.baidu.com/tutorial.html#%E5%9C%A8%20webpack%20%E4%B8%AD%E4%BD%BF%E7%94%A8%20ECharts
    // eslint-disable-next-line no-undef
    Vue.prototype.$echarts = echarts

    // 日期格式化插件
    Vue.prototype.$dayjs = dayjs

    // 全局混入
    Mixins.global(Vue)

    // 加载饿了么框架
    Vue.use(ElementUI)

    // 加载图标
    Vue.use(Icon)

    // 加载自定义组件
    Vue.use(Components)

    // 注册皮肤组件
    Vue.component('nm-skins', Layout)

    // 注册指令
    Vue.use(Directive)

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

    // 处理回调
    if (callbacks) {
      let params = { vm, store, router, Vue }
      callbacks.map(callback => {
        callback(params)
      })
    }

    // 附加自定义样式
    appendCustomCss(system)

    return { router, store, vm }
  }
}

const mixins = Mixins.components
export { mixins, store, token }
