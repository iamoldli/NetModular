/** 页面信息 */
const page = new (function() {
  this.title = '权限列表'
  this.icon = 'verifycode'
  this.name = 'admin_permission'
  this.path = '/admin/permission'

  // 关联权限
  this.permissions = [`${this.name}_query_get`]

  // 按钮
  this.buttons = {
    sync: {
      text: '同步',
      type: 'success',
      icon: 'refresh',
      code: `${this.name}_sync`,
      permissions: [`${this.name}_sync_post`]
    }
  }
})()

/** 路由信息 */
export const route = {
  page,
  component: () => import(/* webpackChunkName: "admin.permission" */ './index')
}

export default page
