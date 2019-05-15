/** 页面信息 */
const page = new function() {
  this.title = '基础表单示例'
  this.name = 'components-form-demo1'
  this.path = '/components/form/demo1'
  this.icon = 'develop'
}()

export const route = {
  page,
  component: () => import('./index')
}

export default page
