/** 页面信息 */
const page = new function() {
  this.title = '滚动条示例'
  this.name = 'components-scrollbar-demo1'
  this.path = '/components/scrollbar/demo1'
  this.icon = 'develop'
}()

export const route = {
  page,
  component: () => import('./index')
}

export default page
