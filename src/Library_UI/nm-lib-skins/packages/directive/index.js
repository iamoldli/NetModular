import drop from './drop'
import resizable from './resizable'
import has from './has'
const directive = [drop, resizable, has]
const prefix = 'Nm'

const install = {
  install: Vue => {
    directive.forEach(o => {
      Vue.directive(`${prefix}${o.name}`, o.directive)
    })
  }
}
export default install
