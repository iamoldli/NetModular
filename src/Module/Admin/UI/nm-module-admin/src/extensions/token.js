import { db } from 'nm-lib-utils'
const TokenKey = 'token'

export default {
  /**
   * 获取令牌
   */
  get () {
    return db.get(TokenKey)
  },
  /**
   * 设置令牌
   * @param {String} token 令牌
   */
  set (token) {
    db.set(TokenKey, token)
  },
  /**
   * 删除令牌
   */
  remove () {
    db.set(TokenKey)
  }
}
