import ElementUI from 'element-ui'
import 'element-ui/lib/theme-chalk/index.css'
import Components from 'nm-lib-skins/packages/components/index.js'
import 'nm-lib-skins/packages/styles/app.scss'

export default ({
  Vue, // VuePress 正在使用的 Vue 构造函数
  options, // 附加到根实例的一些选项
  router, // 当前应用的路由实例
  siteData // 站点元数据
}) => {
  //注册Element-UI
  Vue.use(ElementUI)

  //注册Skins组件
  Vue.use(Components)
}
