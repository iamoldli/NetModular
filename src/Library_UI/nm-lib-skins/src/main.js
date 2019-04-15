import Vue from 'vue'
import routerConfig from './router'
import storeConfig from './store'
import Skins from '../packages/index'
import Mock from 'mockjs'
import './components'
import systemService from './api/system'
import 'highlight.js/styles/github.css'
import VueHighlightJS from 'vue-highlightjs'
Vue.use(VueHighlightJS)

const system = systemService.get()

// 设置个时间，防止等待页面闪烁
setTimeout(() => {
  Skins.use({ system, routerConfig, storeConfig })
}, 1500)

// 添加Mock
Vue.prototype.$mock = options => {
  return Mock.mock(options)
}
Vue.prototype.$random = Mock.Random
