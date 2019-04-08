import { http } from 'nm-lib-utils'
const root = 'admin/role/'
const crud = http.crud(root)
const urls = {
  select: root + 'select',
  menuList: root + 'menulist',
  bindMenu: root + 'bindmenu',
  menuButtonList: root + 'MenuButtonList',
  bindMenuButton: root + 'BindMenuButton'
}

/**
 * @description 下拉框
 */
const select = () => {
  return http.get(urls.select)
}

/**
 * @description 获取绑定的菜单列表
 * @param {*} id 角色编号
 */
const menuList = (id) => {
  return http.get(urls.menuList, { id })
}

/**
 * @description 绑定菜单
 */
const bindMenu = params => {
  return http.post(urls.bindMenu, params)
}

/**
 * @description 获取指定菜单的按钮信息
 */
const menuButtonList = params => {
  return http.get(urls.menuButtonList, params)
}

/**
 * @description 绑定按钮
 */
const bindMenuButton = params => {
  return http.post(urls.bindMenuButton, params)
}

export default {
  ...crud,
  select,
  menuList,
  bindMenu,
  menuButtonList,
  bindMenuButton
}
