/** 页面信息 */
const page = new function() {
  this.title = '列表页示例'
  this.name = 'components-list-demo1'
  this.path = '/components/list/demo1'
  this.icon = 'develop'
}()

export const route = {
  page,
  component: () => import('./index')
}

export default page
