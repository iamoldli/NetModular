/** 页面信息 */
const page = new function() {
  this.title = '模块管理'
  this.icon = 'product'
  this.name = 'admin_moduleinfo'
  this.path = '/admin/moduleinfo'

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
    },
    del: {
      text: '删除',
      type: 'text',
      icon: 'delete',
      code: `${this.name}_del`,
      permissions: [`${this.name}_delete_delete`]
    }
  }
}()

/** 路由信息 */
export const route = {
  page,
  component: () => import(/* webpackChunkName: "admin.moduleinfo" */ './index')
}

export default page
