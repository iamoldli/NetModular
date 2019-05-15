/** 页面信息 */
const page = new function() {
  this.title = '滚动条'
  this.name = 'components-scrollbar-index'
  this.path = '/components/scrollbar/index'
  this.icon = 'card'
  this.sort = 18
}()

export const route = {
  page,
  component: () => import('./index')
}

export default page
