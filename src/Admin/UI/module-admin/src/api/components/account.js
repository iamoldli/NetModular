import module from '../../module'

export default name => {
  const root = `${module.code}/${name}/`
  const crud = $http.crud(root)

  const urls = {
    updatePassword: root + 'updatepassword',
    bindRole: root + 'bindrole',
    resetPassword: root + 'ResetPassword',
    skinUpdate: root + 'SkinUpdate'
  }

  /**
   * @description 修改密码
   */
  const updatePassword = params => {
    return $http.post(urls.updatePassword, params)
  }

  /**
   * @description 绑定角色
   */
  const bindRole = params => {
    return $http.post(urls.bindRole, params)
  }

  /**
   * @description 重置密码
   */
  const resetPassword = id => {
    return $http.post(urls.resetPassword + '?id=' + id)
  }

  /**
   * @description 皮肤修改
   * @param {Object} params
   */
  const skinUpdate = params => {
    return $http.post(urls.skinUpdate, params)
  }

  // 接口集合
  return {
    ...crud,
    updatePassword,
    bindRole,
    resetPassword,
    skinUpdate
  }
}
