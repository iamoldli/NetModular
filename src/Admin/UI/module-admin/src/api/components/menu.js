import module from '../../module'

export default name => {
  const root = `${module.code}/${name}/`
  const crud = $http.crud(root)
  const urls = {
    getTree: root + 'tree',
    getTargetSelect: root + 'targetselect',
    permissionList: root + 'PermissionList',
    bindPermission: root + 'bindPermission',
    buttonList: root + 'buttonlist',
    sort: root + 'sort'
  }

  /**
   * @description 菜单树
   */
  const getTree = () => {
    return $http.get(urls.getTree)
  }

  /**
   * @description 获取菜单打开方式下拉列表
   */
  const getTargetSelect = () => {
    return $http.get(urls.getTargetSelect)
  }

  /**
   * @description 获取权限列表
   */
  const permissionList = id => {
    return $http.get(urls.permissionList, { id })
  }

  /**
   * @description 绑定权限
   */
  const bindPermission = params => {
    return $http.post(urls.bindPermission, params)
  }

  /**
   * @description 获取菜单列表
   */
  const buttonList = id => {
    return $http.get(urls.buttonList, { id })
  }

  const querySortList = parentId => {
    return $http.get(urls.sort, { parentId })
  }

  const updateSortList = params => {
    return $http.post(urls.sort, params)
  }

  return {
    ...crud,
    getTree,
    getTargetSelect,
    permissionList,
    bindPermission,
    buttonList,
    querySortList,
    updateSortList
  }
}
