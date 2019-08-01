import { http } from 'nm-lib-utils'
import { token } from 'nm-lib-skins'

const root = 'admin/account/'
const crud = http.crud(root)
const urls = {
  getVerifyCode: root + 'verifycode',
  login: root + 'login',
  updatePassword: root + 'updatepassword',
  getLoginInfo: root + 'logininfo',
  bindRole: root + 'bindrole',
  resetPassword: root + 'ResetPassword'
}

/*
 * 获取验证码
 */
const getVerifyCode = () => {
  return http.get(urls.getVerifyCode)
}

/**
 * 登录
 * @param {*} params
 */
const login = async params => {
  var data = await http.post(urls.login, params)
  if (data) {
    token.set(data)
  }
  return data
}

/** 退出 */
const logout = () => {
  token.remove()
}

/** 修改密码 */
const updatePassword = params => {
  return http.post(urls.updatePassword, params)
}
/**
 * 获取登录账户信息
 */
const getLoginInfo = () => {
  return http.get(urls.getLoginInfo)
}

/**
 * @description 绑定角色
 */
const bindRole = params => {
  return http.post(urls.bindRole, params)
}

/**
 * @description 重置密码
 */
const resetPassword = id => {
  return http.post(urls.resetPassword + '?id=' + id)
}

export default {
  ...crud,
  getVerifyCode,
  login,
  logout,
  updatePassword,
  getLoginInfo,
  bindRole,
  resetPassword
}
