/** 页面信息 */
const page = new (function() {
  this.title = '缓存管理'
  this.icon = 'redis'
  this.name = 'admin_cache'
  this.path = '/admin/cache'

  // 关联权限
  this.permissions = [`${this.name}_query_get`]

  // 按钮
  this.buttons = {
    clear: {
      text: '清除',
      type: 'text',
      icon: 'delete',
      code: `${this.name}_clear`,
      permissions: [`${this.name}_clear_delete`]
    }
  }
})()

/** 路由信息 */
export const route = {
  page,
  component: () => import(/* webpackChunkName: "admin.cache" */ './index')
}

export default page
