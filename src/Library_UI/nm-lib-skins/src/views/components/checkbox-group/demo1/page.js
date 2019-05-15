/** 页面信息 */
const page = new function () {
  this.title = '多选框组件'
  this.name = 'components-checkbox-group-demo1'
  this.path = '/components/checkbox-group/demo1'
  this.icon = 'develop'
}()

export const route = {
  page,
  component: () => import('./index')
}

export default page
