/** 页面信息 */
const page = new function() {
  this.title = '多选框'
  this.name = 'components-checkbox-group-index'
  this.path = '/components/checkbox-group/index'
  this.icon = 'checkbox'
  this.sort = 4
}()

export const route = {
  page,
  component: () => import('./index')
}

export default page
