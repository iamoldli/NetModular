/** 页面信息 */
const page = new function() {
  this.title = '输入框'
  this.name = 'components-input-index'
  this.path = '/components/input/index'
  this.icon = 'card'
  this.sort = 19
}()

export const route = {
  page,
  component: () => import('./index')
}

export default page
