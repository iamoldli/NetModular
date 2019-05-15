/** 页面信息 */
const page = new function() {
  this.title = '拖拽排序示例'
  this.name = 'components-drag-sort-demo1'
  this.path = '/components/drag-sort/demo1'
  this.icon = 'develop'
}()

export const route = {
  page,
  component: () => import('./index')
}

export default page
