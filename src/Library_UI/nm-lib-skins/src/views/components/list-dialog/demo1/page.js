/** 页面信息 */
const page = new function() {
  this.title = '列表页对话框示例'
  this.name = 'components-list-dialog-demo1'
  this.path = '/components/list-dialog/demo1'
  this.icon = 'develop'
}()

export const route = {
  page,
  component: () => import('./index')
}

export default page
