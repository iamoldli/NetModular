import { http } from 'nm-lib-utils'
const root = 'admin/config/'
const crud = http.crud(root)
const urls = {
  systemConfig: root + 'systemconfig',
  systemConfigUpdate: root + 'SystemConfigUpdate'
}
/**
 * @description 获取系统配置
 */
const getSystemConfig = () => {
  return http.get(urls.systemConfig)
}

/**
 * @description 修改系统配置
 */
const updateSystemConfig = params => {
  return http.post(urls.systemConfigUpdate, params)
}

export default {
  ...crud,
  getSystemConfig,
  updateSystemConfig
}
