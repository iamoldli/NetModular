/** 页面信息 */
const page = new function() {
  this.title = '页表单示例'
  this.name = 'components-form-page-demo1'
  this.path = '/components/form-page/demo1'
  this.icon = 'develop'
}()

export const route = {
  page,
  component: () => import('./index')
}

export default page
