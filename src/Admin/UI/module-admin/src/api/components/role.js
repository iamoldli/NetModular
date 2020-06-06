import module from '../../module'

export default name => {
  const root = `${module.code}/${name}/`
  const crud = $http.crud(root)
  const urls = {
    select: root + 'select',
    bindPages: root + 'BindPages',
    bindMenus: root + 'BindMenus',
    bindPlatformPermissions: root + 'BindPlatformPermissions'
  }

  /**
   * @description 下拉框
   */
  const select = () => {
    return $http.get(urls.select)
  }

  /**
   * @description 获取绑定的页面列表
   * @param {*} id 角色编号
   */
  const queryBindPages = id => {
    return $http.get(urls.bindPages, { id })
  }

  /**
   * @description 绑定页面
   */
  const bindPages = params => {
    return $http.post(urls.bindPages, params)
  }

  /**
   * @description 获取绑定的菜单列表
   * @param {*} id 角色编号
   */
  const queryBindMenus = id => {
    return $http.get(urls.bindMenus, { id })
  }

  /**
   * @description 绑定页面
   */
  const bindMenus = params => {
    return $http.post(urls.bindMenus, params)
  }

  /**
   * @description 获取绑定的平台权限列表
   */
  const queryBindPlatformPermissions = params => {
    return $http.get(urls.bindPlatformPermissions, params)
  }

  /**
   * @description 绑定页面
   */
  const bindPlatformPermissions = params => {
    return $http.post(urls.bindPlatformPermissions, params)
  }
  return {
    ...crud,
    select,
    queryBindPages,
    bindPages,
    queryBindMenus,
    bindMenus,
    queryBindPlatformPermissions,
    bindPlatformPermissions
  }
}
