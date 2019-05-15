import Vue from 'vue'
import routerConfig from './router'
import storeConfig from './store'
import Skins from '../packages/index'
import Mock from 'mockjs'
import './components'
import systemService from './api/system'
import 'highlight.js/styles/darkula.css'
import VueHighlightJS from 'vue-highlightjs'
import Vuep from 'vuep'
import 'vuep/dist/vuep.css'
import './style/element-ui.scss'

// 代码高亮插件
Vue.use(VueHighlightJS)

// 在线编辑预览代码插件
Vue.use(Vuep /*, { codemirror options } */)

// 获取系统信息
const system = systemService.get()

// 设置个时间，防止等待页面闪烁
setTimeout(() => {
  Skins.use({ system, routerConfig, storeConfig })
}, 500)

// 添加Mock
Vue.prototype.$mock = options => {
  return Mock.mock(options)
}
Vue.prototype.$random = Mock.Random
