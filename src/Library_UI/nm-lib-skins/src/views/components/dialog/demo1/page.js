/** 页面信息 */
const page = new function () {
  this.title = '对话框示例'
  this.name = 'components-dialog-demo1'
  this.path = '/components/dialog/demo1'
  this.icon = 'develop'
}()

export const route = {
  page,
  component: () => import('./index')
}

export default page
