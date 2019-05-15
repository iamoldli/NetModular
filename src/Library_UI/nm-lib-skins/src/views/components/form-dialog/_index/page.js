/** 页面信息 */
const page = new function() {
  this.title = '对话框表单'
  this.name = 'components-form-dialog-index'
  this.path = '/components/form-dialog/index'
  this.icon = 'list'
  this.sort = 12
}()

export const route = {
  page,
  component: () => import('./index')
}

export default page
