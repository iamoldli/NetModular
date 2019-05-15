/** 页面信息 */
const page = new function() {
  this.title = '面板分割'
  this.name = 'components-split-index'
  this.path = '/components/split/index'
  this.icon = 'card'
  this.sort = 20
}()

export const route = {
  page,
  component: () => import('./index')
}

export default page
