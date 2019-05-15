/** 页面信息 */
const page = new function () {
  this.title = '盒子页'
  this.name = 'components-box-demo4'
  this.path = '/components/box/demo4'
  this.icon = 'develop'
}()

export const route = {
  page,
  component: () => import('./index')
}

export default page
