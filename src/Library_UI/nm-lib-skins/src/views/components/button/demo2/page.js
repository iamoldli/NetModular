/** 页面信息 */
const page = new function () {
  this.title = '复杂按钮'
  this.name = 'components-button-demo2'
  this.path = '/components/button/demo2'
  this.icon = 'develop'
}()

export const route = {
  page,
  component: () => import('./index')
}

export default page
