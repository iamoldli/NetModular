/** 页面信息 */
const page = new (function() {
  this.title = '图标管理'
  this.name = 'admin_icon'
  this.path = '/admin/icon'
})()

/** 路由信息 */
export const route = {
  page,
  component: () => import(/* webpackChunkName: "admin.icon" */ './index')
}

export default page
