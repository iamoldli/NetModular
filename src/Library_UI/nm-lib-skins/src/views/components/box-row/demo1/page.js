/** 页面信息 */
const page = new function() {
  this.title = '盒子栅格化示例'
  this.name = 'components-box-row-demo1'
  this.path = '/components/box-row/demo1'
  this.icon = 'develop'
}()

export const route = {
  page,
  component: () => import('./index')
}

export default page
