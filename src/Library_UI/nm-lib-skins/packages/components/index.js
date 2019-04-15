let components = []
const requireComponent = require.context('../components', true, /\.js|.vue$/)
requireComponent
  .keys()
  .filter(item => item !== './index.js')
  .forEach(fileName => {
    const componentConfig = requireComponent(fileName)
    if (componentConfig.default.name) {
      components.push(componentConfig.default)
    }
  })
export default function(Vue) {
  // 前缀
  const prefix = 'Nm'

  components.forEach(component => {
    Vue.component(`${prefix}${component.name}`, component)
  })
}
