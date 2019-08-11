import { http } from 'nm-lib-utils'
const root = 'Common/Attachment/'
const crud = http.crud(root)
const urls = {
  upload: root + 'Upload',
  download: root + 'Download/'
}

const getUploadUrl = () => {
  return http.axios.defaults.baseURL + urls.upload
}
// 下载
const download = id => {
  return http.get(urls.download + id, {}, { responseType: 'blob' })
}

// 预览
const preview = id => {
  return http.get(
    urls.download + id,
    {},
    { responseType: 'blob', preview: true }
  )
}

export default {
  ...crud,
  getUploadUrl,
  download,
  preview
}
