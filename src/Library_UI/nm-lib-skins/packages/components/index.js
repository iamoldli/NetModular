import lib from '../library'

let components = []
const requireComponent = require.context('../components', true, /index\.vue$/)
requireComponent
  .keys()
  .filter(item => item !== './index.js')
  .forEach(fileName => {
    const componentConfig = requireComponent(fileName)
    if (componentConfig.default && componentConfig.default.name) {
      components.push(componentConfig.default)
    }
  })
export default function(Vue) {
  components.forEach(component => {
    Vue.component(`${lib.prefix}${component.name}`, component)
  })
}
