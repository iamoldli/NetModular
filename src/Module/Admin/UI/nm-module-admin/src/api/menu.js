import { http } from 'nm-lib-utils'
const root = 'admin/menu/'
const crud = http.crud(root)
const urls = {
  getTree: root + 'tree',
  getTargetSelect: root + 'targetselect',
  details: root + 'details',
  permissionList: root + 'PermissionList',
  bindPermission: root + 'bindPermission',
  buttonList: root + 'buttonlist'
}

/**
 * @description 菜单树
 */
const getTree = () => {
  return http.get(urls.getTree)
}

/**
 * @description 获取菜单打开方式下拉列表
 */
const getTargetSelect = () => {
  return http.get(urls.getTargetSelect)
}

/**
 * @description 详情
 */
const details = id => {
  return http.get(urls.details, { id })
}

/**
 * @description 获取权限列表
 */
const permissionList = id => {
  return http.get(urls.permissionList, { id })
}

/**
 * @description 绑定权限
 */
const bindPermission = params => {
  return http.post(urls.bindPermission, params)
}

/**
 * @description 获取菜单列表
 */
const buttonList = id => {
  return http.get(urls.buttonList, { id })
}

export default {
  ...crud,
  getTree,
  getTargetSelect,
  details,
  permissionList,
  bindPermission,
  buttonList
}
