import { token } from 'nm-lib-skins'
import module from '../../module'

export default name => {
  const root = `${module.code}/${name}/`
  const crud = $http.crud(root)

  const urls = {
    getVerifyCode: root + 'verifycode',
    login: root + 'login',
    updatePassword: root + 'updatepassword',
    getLoginInfo: root + 'logininfo',
    bindRole: root + 'bindrole',
    resetPassword: root + 'ResetPassword',
    skinUpdate: root + 'SkinUpdate'
  }

  /**
   * @description 获取验证码
   */
  const getVerifyCode = () => {
    return $http.get(urls.getVerifyCode)
  }

  /**
   * @description 登录
   * @param {*} params
   */
  const login = async params => {
    var data = await $http.post(urls.login, params)
    if (data) {
      token.set(data)
    }
    return data
  }

  /**
   * @description 退出
   */
  const logout = () => {
    token.remove()
  }

  /**
   * @description 修改密码
   */
  const updatePassword = params => {
    return $http.post(urls.updatePassword, params)
  }

  /**
   * @description 获取登录账户信息
   */
  const getLoginInfo = () => {
    return $http.get(urls.getLoginInfo)
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
    getVerifyCode,
    login,
    logout,
    updatePassword,
    getLoginInfo,
    bindRole,
    resetPassword,
    skinUpdate
  }
}
