/** 页面信息 */
const page = new function() {
  this.title = '首页'
  this.name = 'home'
  this.path = '/home'
  this.icon = 'home'
}()

export const route = {
  page,
  component: () => import('./index')
}

export default page
