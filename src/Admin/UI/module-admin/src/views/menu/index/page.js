/** 页面信息 */
const page = new (function() {
  this.title = '菜单管理'
  this.icon = 'menu'
  this.name = 'admin_menu'
  this.path = '/admin/menu'
  // 权限
  this.permissions = [`${this.name}_query_get`, `${this.name}_tree_get`]
  // 按钮
  this.buttons = {
    add: {
      text: '添加',
      icon: 'add',
      type: 'success',
      code: `${this.name}_add`,
      permissions: [`${this.name}_add_post`]
    },
    edit: {
      text: '编辑',
      icon: 'edit',
      type: 'text',
      code: `${this.name}_edit`,
      permissions: [`${this.name}_edit_Get`, `${this.name}_update_post`]
    },
    del: {
      text: '删除',
      icon: 'delete',
      type: 'text',
      code: `${this.name}_del`,
      permissions: [`${this.name}_delete_delete`]
    },
    sort: {
      text: '排序',
      icon: 'sort',
      type: 'warning',
      code: `${this.name}_sort`,
      permissions: [`${this.name}_sort_get`, `${this.name}_sort_post`]
    }
  }
})()

/** 路由信息 */
export const route = {
  page,
  component: () => import(/* webpackChunkName: "admin.menu" */ './index')
}

export default page
