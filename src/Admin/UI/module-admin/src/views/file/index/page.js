/** 页面信息 */
const page = new (function() {
  this.title = '文件管理'
  this.icon = 'attachment'
  this.name = 'admin_file'
  this.path = '/admin/file'

  // 关联权限
  this.permissions = [`${this.name}_query_get`]

  this.buttons = {
    export: {
      text: '导出',
      type: 'text',
      icon: 'export',
      code: `${this.name}_export`,
      permissions: [`${this.name}_export_get`]
    },
    del: {
      text: '删除',
      type: 'text',
      icon: 'delete',
      code: `${this.name}_del`,
      permissions: [`${this.name}_delete_delete`]
    },
    hardDel: {
      text: '物理删除',
      type: 'text',
      icon: 'delete',
      code: `${this.name}_harddel`,
      permissions: [`${this.name}_hardDelete_delete`]
    }
  }
})()

/** 路由信息 */
export const route = {
  page,
  component: () => import(/* webpackChunkName: "Admin.File" */ './index')
}

export default page
