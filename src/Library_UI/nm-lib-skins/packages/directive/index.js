import lib from '../library'
import has from './has'

const directive = [has]

const install = {
  install: Vue => {
    directive.forEach(o => {
      Vue.directive(`${lib.prefix}${o.name}`, o.directive)
    })
  }
}
export default install
