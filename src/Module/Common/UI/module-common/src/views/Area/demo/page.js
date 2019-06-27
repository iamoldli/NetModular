/** 页面信息 */
const page = new function() {
  this.title = '区划代码组件示例'
  this.name = 'Common_Area_Demo'
  this.path = '/Common/Area/Demo'
  this.buttons = {}
}()

/** 路由信息 */
export const route = {
  page,
  component: () => import(/* webpackChunkName: "Common.Area.Demo" */ './index')
}

export default page
