/** 页面信息 */
const page = new function() {
  this.title = '单文件上传示例'
  this.name = 'components-upload-single-demo1'
  this.path = '/components/upload-single/demo1'
  this.icon = 'develop'
}()

export const route = {
  page,
  component: () => import('./index')
}

export default page
