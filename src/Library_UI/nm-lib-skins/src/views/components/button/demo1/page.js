/** 页面信息 */
const page = new function () {
  this.title = '简单按钮'
  this.name = 'components-button-demo1'
  this.path = '/components/button/demo1'
  this.icon = 'develop'
}()

export const route = {
  page,
  component: () => import('./index')
}

export default page
