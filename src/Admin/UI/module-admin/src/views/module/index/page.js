/** 页面信息 */
const page = new (function() {
  this.title = '模块中心'
  this.icon = 'product'
  this.name = 'admin_module'
  this.path = '/admin/module'

  // 关联权限
  this.permissions = [`${this.name}_query_get`, `admin_permission_querybycodes_get`]

  // 按钮
  this.buttons = {}
})()

/** 路由信息 */
export const route = {
  page,
  component: () => import(/* webpackChunkName: "admin.module" */ './index')
}

export default page
