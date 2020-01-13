import module from '../../module'

export default name => {
  const root = `${module.code}/${name}/`
  const crud = $http.crud(root)
  const urls = {
    select: root + 'select',
    menuList: root + 'menulist',
    bindMenu: root + 'bindmenu',
    menuButtonList: root + 'MenuButtonList',
    bindMenuButton: root + 'BindMenuButton',
    platformPermissionList: root + 'PlatformPermissionList',
    platformPermissionBind: root + 'PlatformPermissionBind'
  }

  /**
   * @description 下拉框
   */
  const select = () => {
    return $http.get(urls.select)
  }

  /**
   * @description 获取绑定的菜单列表
   * @param {*} id 角色编号
   */
  const menuList = id => {
    return $http.get(urls.menuList, { id })
  }

  /**
   * @description 绑定菜单
   */
  const bindMenu = params => {
    return $http.post(urls.bindMenu, params)
  }

  /**
   * @description 获取指定菜单的按钮信息
   */
  const menuButtonList = params => {
    return $http.get(urls.menuButtonList, params)
  }

  /**
   * @description 绑定按钮
   */
  const bindMenuButton = params => {
    return $http.post(urls.bindMenuButton, params)
  }

  /**
   * @description 查询平台权限列表
   */
  const platformPermissionList = params => {
    return $http.get(urls.platformPermissionList, params)
  }

  /**
   * @description 绑定平台权限
   */
  const platformPermissionBind = params => {
    return $http.post(urls.platformPermissionBind, params)
  }

  return {
    ...crud,
    select,
    menuList,
    bindMenu,
    menuButtonList,
    bindMenuButton,
    platformPermissionList,
    platformPermissionBind
  }
}
