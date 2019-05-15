// 全局混入
import message from './global/message'
import fontSize from './global/fontSize'
import setTabName from './global/setTabName'

// 局部混入
import dialog from './components/dialog'
import select from './components/select'
import drawer from './components/drawer'
import formDialogEdit from './components/form-dialog-edit'
import loading from './components/loading'

export default {
  global (Vue) {
    // 消息提示
    Vue.mixin(message)

    // 字号
    Vue.mixin(fontSize)

    // 设置标签页名称
    Vue.mixin(setTabName)
  },
  components: {
    dialog,
    select,
    drawer,
    formDialogEdit,
    loading
  }
}
