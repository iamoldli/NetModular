/** 页面信息 */
const page = new function() {
  this.title = '首页'
  this.icon = 'home'
  this.name = 'home'
  this.path = '/admin/home'
  this.icon = 'home'
}()

export const route = {
  page,
  component: () => import('./index')
}

export default page
