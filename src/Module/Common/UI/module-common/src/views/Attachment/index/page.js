/** 页面信息 */
const page = new function() {
  this.title = '附件管理'
  this.icon = 'attachment'
  this.name = 'common_attachment'
  this.path = '/common/attachment'

  // 关联权限
  this.permissions = [`${this.name}_query_get`]

  // 按钮
  this.buttons = {
    del: {
      text: '删除',
      type: 'text',
      icon: 'delete',
      code: `${this.name}_del`,
      permissions: [`${this.name}_delete_delete`]
    },
    export: {
      text: '导出',
      type: 'text',
      icon: 'export',
      code: `${this.name}_export`,
      permissions: [`${this.name}_export_get`]
    }
  }
}()

/** 路由信息 */
export const route = {
  page,
  component: () => import(/* webpackChunkName: "Common.Attachment" */ './index')
}

export default page
