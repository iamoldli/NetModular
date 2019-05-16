import logo from '../../packages/assets/logo.png'
import token from './token'
import routeConfig from '../router'
import accountApi from './account'
const get = () => {
  return {
    /** 标题 */
    title: '通用后台管理系统',
    /** logo */
    logo,
    /** 默认页 */
    home: '', // 退出方法
    logout() {
      token.remove()
      routeConfig.$router.push({ name: 'login' })
    },
    // 修改密码
    updatePassword() {
      return new Promise((resolve, reject) => {
        resolve()
      })
    },
    getLoginInfo: accountApi.get
  }
}

export default {
  get
}
