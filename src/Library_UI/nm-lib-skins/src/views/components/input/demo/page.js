/** 页面信息 */
const page = new function() {
  this.title = '输入框示例'
  this.name = 'components-input-demo'
  this.path = '/components/input/demo'
  this.icon = 'develop'
}()

export const route = {
  page,
  component: () => import('./index')
}

export default page
