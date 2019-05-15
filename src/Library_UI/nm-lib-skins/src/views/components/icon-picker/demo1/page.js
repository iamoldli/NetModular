/** 页面信息 */
const page = new function() {
  this.title = '图标选择器示例'
  this.name = 'components-icon-picker-demo1'
  this.path = '/components/icon-picker/demo1'
  this.icon = 'develop'
}()

export const route = {
  page,
  component: () => import('./index')
}

export default page
