/** 页面信息 */
const page = new function() {
  this.title = '抽屉'
  this.name = 'components-drawer-index'
  this.path = '/components/drawer/index'
  this.icon = 'list'
  this.sort = 10
}()

export const route = {
  page,
  component: () => import('./index')
}

export default page
