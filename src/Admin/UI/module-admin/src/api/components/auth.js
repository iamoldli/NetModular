import module from '../../module'

export default name => {
  const root = `${module.code}/${name}/`
  const urls = {
    getVerifyCode: root + 'VerifyCode',
    login: root + 'Login',
    refreshToken: root + 'RefreshToken',
    getAuthInfo: root + 'AuthInfo'
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
    return $http.post(urls.login, params)
  }

  /**
   * @description 刷新令牌
   */
  const refreshToken = refreshToken => {
    return $http.get(urls.refreshToken, { refreshToken })
  }

  /**
   * @description 获取登录账户信息
   */
  const getAuthInfo = () => {
    return $http.get(urls.getAuthInfo)
  }

  return {
    getVerifyCode,
    login,
    refreshToken,
    getAuthInfo
  }
}
