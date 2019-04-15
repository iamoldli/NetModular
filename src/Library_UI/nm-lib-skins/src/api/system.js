import logo from '../../packages/assets/logo.png'
import token from './token'
import routeConfig from '../router'
const get = () => {
  return {
    /** 标题 */
    title: 'Vue.js快速开发框架',
    /** logo */
    logo,
    /** 默认页 */
    default: '/demo', // 退出方法
    logout () {
      token.remove()
      routeConfig.$router.push({ name: 'login' })
    },
    // 修改密码
    updatePassword () {
      return new Promise((resolve, reject) => {
        resolve()
      })
    }
  }
}

export default {
  get
}
