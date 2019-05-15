/** 页面信息 */
const page = new function () {
  this.title = '有头有尾'
  this.name = 'components-box-demo2'
  this.path = '/components/box/demo2'
  this.icon = 'develop'
}()

export const route = {
  page,
  component: () => import('./index')
}

export default page
