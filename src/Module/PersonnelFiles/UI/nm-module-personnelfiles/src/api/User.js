import { http } from 'nm-lib-utils'
const root = 'PersonnelFiles/User/'
const crud = http.crud(root)
const urls = {
  uploadPicture: root + 'UploadPicture'
}

/**
 * 获取上传照片接口地址
 */
const getUploadPictureUrl = () => {
  return http.axios.defaults.baseURL + urls.uploadPicture
}
export default {
  ...crud,
  getUploadPictureUrl
}
