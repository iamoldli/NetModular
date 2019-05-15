/** 页面信息 */
const page = new function() {
  this.title = '下拉列表'
  this.name = 'components-select-index'
  this.path = '/components/select/index'
  this.icon = 'card'
  this.sort = 19
}()

export const route = {
  page,
  component: () => import('./index')
}

export default page
