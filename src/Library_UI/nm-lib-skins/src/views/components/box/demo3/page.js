/** 页面信息 */
const page = new function () {
  this.title = '工具栏'
  this.name = 'components-box-demo3'
  this.path = '/components/box/demo3'
  this.icon = 'develop'
}()

export const route = {
  page,
  component: () => import('./index')
}

export default page
