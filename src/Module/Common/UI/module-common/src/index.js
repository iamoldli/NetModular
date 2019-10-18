import './api'
import store from './store'
import routes from './routes'
import components from './components'
import module from './module'

export default {
  module,
  routes,
  store,
  components,
  callback({ Vue }) {
    // 附件上传地址
    Vue.prototype.$attachment = {
      // 上传地址
      uploadUrl: $api.common.attachment.getUploadUrl(),
      // 下载方法
      download(id) {
        $api.common.attachment.download(id)
      }
    }
  }
}
