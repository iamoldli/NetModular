import dialog from './components/dialog'
import formLoading from './components/formLoading'
import message from './global/message'
import fontSize from './global/fontSize'
import select from './components/select'
import emitter from './components/emitter'
import drawer from './components/drawer'

export default {
  global (Vue) {
    // 消息提示
    Vue.mixin(message)

    // 字号
    Vue.mixin(fontSize)
  },
  components: {
    dialog,
    formLoading,
    select,
    emitter,
    drawer
  }
}
