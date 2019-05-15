/** 页面信息 */
const page = new function() {
  this.title = '列表框'
  this.name = 'components-listbox-index'
  this.path = '/components/listbox/index'
  this.icon = 'card'
  this.sort = 17
}()

export const route = {
  page,
  component: () => import('./index')
}

export default page
