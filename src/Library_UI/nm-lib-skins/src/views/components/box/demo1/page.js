/** 页面信息 */
const page = new function() {
  this.title = '简单盒子'
  this.name = 'components-box-demo1'
  this.path = '/components/box/demo1'
  this.icon = 'develop'
}()

export const route = {
  page,
  component: () => import('./index')
}

export default page
