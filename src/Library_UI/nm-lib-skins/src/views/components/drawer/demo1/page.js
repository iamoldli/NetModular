/** 页面信息 */
const page = new function() {
  this.title = '抽屉示例'
  this.name = 'components-drawer-demo1'
  this.path = '/components/drawer/demo1'
  this.icon = 'develop'
}()

export const route = {
  page,
  component: () => import('./index')
}

export default page
