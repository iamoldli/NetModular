import Cookies from 'js-cookie'
const cookie = {}
/**
 * @description 存储 cookie 值
 * @param {String} name 名称
 * @param {String} value 值
 * @param {Object} setting 设置，默认有效期1天
 */
cookie.set = function(name = 'default', value = '', cookieSetting = {}) {
  let currentCookieSetting = {
    expires: 1
  }
  Object.assign(currentCookieSetting, cookieSetting)
  Cookies.set(`nm-${name}`, value, currentCookieSetting)
}

/**
 * @description 拿到 cookie 值
 * @param {String} name 名称
 */
cookie.get = function(name = 'default') {
  return Cookies.get(`nm-${name}`)
}

/**
 * @description 拿到 cookie 全部的值
 */
cookie.getAll = function() {
  return Cookies.get()
}

/**
 * @description 删除 cookie
 * @param {String} name 名称
 */
cookie.remove = function(name = 'default') {
  return Cookies.remove(`nm-${name}`)
}

export default cookie
