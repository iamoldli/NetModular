/** 页面信息 */
const page = new function() {
  this.title = '对话框表单示例'
  this.name = 'components-form-dialog-demo1'
  this.path = '/components/form-dialog/demo1'
  this.icon = 'develop'
}()

export const route = {
  page,
  component: () => import('./index')
}

export default page
