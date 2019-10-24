/** 页面信息 */
const page = new function() {
  this.title = '登录'
  this.name = 'login'
  this.path = '/admin/account/login'
  this.frameIn = false
  this.cache = false
}()

export const route = {
  page,
  component: () => import(/* webpackChunkName: "admin.login" */ './index')
}

export default page
