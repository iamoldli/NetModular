/** 页面信息 */
const page = new (function() {
  this.title = '登录日志'
  this.icon = 'log'
  this.name = 'admin_log_login'
  this.path = '/admin/log/login'

  // 关联权限
  this.permissions = [`${this.name}_get`]
  this.buttons = {
    export: {
      text: '导出',
      type: 'text',
      icon: 'export',
      code: `${this.name}_export`,
      permissions: [`${this.name}export_post`]
    }
  }
})()

/** 路由信息 */
export const route = {
  page,
  component: () => import(/* webpackChunkName: "admin.log" */ './index')
}

export default page
