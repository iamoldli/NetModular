/** 页面信息 */
const page = new (function() {
  this.title = '审计日志'
  this.icon = 'log'
  this.name = 'admin_auditinfo'
  this.path = '/admin/auditinfo'

  // 关联权限
  this.permissions = [`${this.name}_query_get`]
  this.buttons = {
    details: {
      text: '详情',
      type: 'text',
      icon: 'detail',
      code: `${this.name}_details`,
      permissions: [`${this.name}_details_get`]
    },
    export: {
      text: '导出',
      type: 'text',
      icon: 'export',
      code: `${this.name}_export`,
      permissions: [`${this.name}_export_post`]
    }
  }
})()

/** 路由信息 */
export const route = {
  page,
  component: () => import(/* webpackChunkName: "admin.auditinfo" */ './index')
}

export default page
