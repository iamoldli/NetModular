/** 页面信息 */
const page = new function() {
  this.title = '敏捷列表页'
  this.name = 'components-list-demo2'
  this.path = '/components/list/demo2'
  this.icon = 'develop'
}()

export const route = {
  page,
  component: () => import('./index')
}

export default page
