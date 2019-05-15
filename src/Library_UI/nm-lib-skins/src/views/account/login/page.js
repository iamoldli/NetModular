/** 页面信息 */
const page = new function () {
  this.title = '登录'
  this.name = 'Login'
  this.path = '/login'
  this.frameIn = false
  this.cache = false
}()

export const route = {
  page,
  component: () => import('./index')
}

export default page
