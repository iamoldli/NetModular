import store from './store'
import routes from './routes'
import components from './components'
import module from './module'
import api from './api/Attachment'

export default {
  module,
  routes,
  store,
  components,
  callback({ Vue }) {
    // 附件上传地址
    Vue.prototype.$attachment = {
      uploadUrl: api.getUploadUrl()
    }
  }
}
