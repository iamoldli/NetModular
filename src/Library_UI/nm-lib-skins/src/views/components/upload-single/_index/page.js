/** 页面信息 */
const page = new function() {
  this.title = '单文件上传'
  this.name = 'components-upload-single-index'
  this.path = '/components/upload-single/index'
  this.icon = 'card'
  this.sort = 22
}()

export const route = {
  page,
  component: () => import('./index')
}

export default page
