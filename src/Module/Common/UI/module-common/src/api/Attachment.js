import { http } from 'nm-lib-utils'
const root = 'Common/Attachment/'
const crud = http.crud(root)
const urls = {
  upload: root + 'Upload',
  download: root + 'Download/',
  export: root + 'Export/'
}

const getUploadUrl = () => {
  return http.axios.defaults.baseURL + urls.upload
}

const download = id => {
  return http.get(
    urls.download + id,
    {},
    { responseType: 'blob', preview: true }
  )
}

const exportFile = id => {
  return http.get(urls.export + id, {}, { responseType: 'blob' })
}

export default {
  ...crud,
  getUploadUrl,
  download,
  exportFile
}
