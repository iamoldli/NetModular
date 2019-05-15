/** 页面信息 */
const page = new function() {
  this.title = '面板分割示例'
  this.name = 'components-split-demo1'
  this.path = '/components/split/demo1'
  this.icon = 'develop'
}()

export const route = {
  page,
  component: () => import('./index')
}

export default page
