/** 页面信息 */
const page = new function () {
  this.title = '颜色选择器示例'
  this.name = 'components-color-picker-demo1'
  this.path = '/components/color-picker/demo1'
  this.icon = 'develop'
}()

export const route = {
  page,
  component: () => import('./index')
}

export default page
