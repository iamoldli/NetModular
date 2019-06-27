import { http } from 'nm-lib-utils'
const root = 'admin/system/'
const crud = http.crud(root)
const urls = {
  config: root + 'config',
  uploadLogo: root + 'UploadLogo',
  allController: root + 'AllController',
  allAction: root + 'AllAction'
}

/**
 * @description 获取系统配置
 */
const getConfig = () => {
  return http.get(urls.config)
}

/**
 * @description 修改系统配置
 */
const updateConfig = params => {
  return http.post(urls.config, params)
}

/**
 * logo上传接口
 */

const uploadLogoUrl = () => {
  return http.axios.defaults.baseURL + urls.uploadLogo
}

/** 获取指定模块的所有Controller的下拉列表 */
const getAllController = params => {
  return http.get(urls.allController, params)
}

/** 获取指定Controller的下拉列表 */
const getAllAction = params => {
  return http.get(urls.allAction, params)
}

export default {
  ...crud,
  getConfig,
  updateConfig,
  uploadLogoUrl,
  getAllController,
  getAllAction
}
