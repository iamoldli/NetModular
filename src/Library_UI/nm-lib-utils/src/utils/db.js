import store from 'store'
const prefix = 'nm-'
export default {
  /**
   * @description 设置
   */
  set(key, value) {
    store.set(`${prefix}${key}`, value)
  },
  /**
   * @description 获取
   */
  get(key) {
    return store.get(`${prefix}${key}`)
  },
  /**
   * @description 删除令牌
   */
  remove(key) {
    store.remove(`${prefix}${key}`)
  },
  /**
   * @description 清除全部
   */
  clearAll() {
    store.each(function(value, key) {
      if (key.startsWith(prefix)) {
        store.remove(key)
      }
    })
  }
}
