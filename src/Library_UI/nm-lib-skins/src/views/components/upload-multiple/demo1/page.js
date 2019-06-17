/** 页面信息 */
const page = new function() {
  this.title = '多文件上传示例'
  this.name = 'components-upload-multiple-demo1'
  this.path = '/components/upload-multiple/demo1'
  this.icon = 'develop'
}()

export const route = {
  page,
  component: () => import('./index')
}

export default page
