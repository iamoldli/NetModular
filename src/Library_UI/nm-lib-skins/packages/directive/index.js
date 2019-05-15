import lib from '../library'
import drop from './drop'
import resizable from './resizable'
import has from './has'

const directive = [drop, resizable, has]

const install = {
  install: Vue => {
    directive.forEach(o => {
      Vue.directive(`${lib.prefix}${o.name}`, o.directive)
    })
  }
}
export default install
