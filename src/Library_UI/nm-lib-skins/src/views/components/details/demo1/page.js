/** 页面信息 */
const page = new function() {
  this.title = '详情页实例'
  this.name = 'components-details-demo1'
  this.path = '/components/details/demo1'
  this.icon = 'develop'
}()

export const route = {
  page,
  component: () => import('./index')
}

export default page
