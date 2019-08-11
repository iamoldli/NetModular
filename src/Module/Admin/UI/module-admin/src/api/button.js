import { http } from 'nm-lib-utils'
const root = 'admin/button/'
const crud = http.crud(root)
const urls = {
  getPermissionList: root + 'PermissionList',
  bindPermission: root + 'bindPermission',
  sync: root + 'sync'
}

/**
 * @description 获取权限列表
 */
const getPermissionList = id => {
  return http.get(urls.getPermissionList, {
    id
  })
}

/**
 * @description 绑定权限
 */
const bindPermission = params => {
  return http.post(urls.bindPermission, params)
}

/**
 * @description 同步
 */
const sync = params => {
  return http.post(urls.sync, params)
}

export default {
  ...crud,
  getPermissionList,
  bindPermission,
  sync
}
