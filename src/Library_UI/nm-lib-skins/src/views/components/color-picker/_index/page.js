/** 页面信息 */
const page = new function() {
  this.title = '颜色选择器'
  this.name = 'components-color-picker-index'
  this.path = '/components/color-picker/index'
  this.icon = 'list'
  this.sort = 5
}()

export const route = {
  page,
  component: () => import('./index')
}

export default page
