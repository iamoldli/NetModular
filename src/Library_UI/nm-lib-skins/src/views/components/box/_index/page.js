/** 页面信息 */
const page = new function() {
  this.title = '盒子'
  this.name = 'components-box-index'
  this.path = '/components/box/index'
  this.icon = 'card'
  this.sort = 1
}()

export const route = {
  page,
  component: () => import('./index')
}

export default page
