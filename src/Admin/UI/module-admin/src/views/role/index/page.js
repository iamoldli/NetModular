/** 页面信息 */
const page = new (function() {
  this.title = '角色管理'
  this.icon = 'role'
  this.name = 'admin_role'
  this.path = '/admin/role'

  // 关联权限
  this.permissions = [`${this.name}_query_get`]

  // 按钮
  this.buttons = {
    add: {
      text: '添加',
      type: 'text',
      icon: 'add',
      code: `${this.name}_add`,
      permissions: [`${this.name}_add_post`]
    },
    edit: {
      text: '编辑',
      type: 'text',
      icon: 'edit',
      code: `${this.name}_edit`,
      permissions: [`${this.name}_edit_get`, `${this.name}_update_post`]
    },
    del: {
      text: '删除',
      type: 'text',
      icon: 'delete',
      code: `${this.name}_del`,
      permissions: [`${this.name}_delete_delete`]
    },
    bindMenus: {
      text: '菜单绑定',
      type: 'text',
      icon: 'bind',
      code: `${this.name}_bind_menus`,
      permissions: [`${this.name}_BindMenus_get`, `${this.name}_BindMenus_post`]
    },
    bindPages: {
      text: '页面授权',
      type: 'text',
      icon: 'bind',
      code: `${this.name}_bind_pages`,
      permissions: [`${this.name}_BindPages_get`, `${this.name}_BindPages_post`]
    },
    bindPlatform: {
      text: '平台授权',
      type: 'text',
      icon: 'bind',
      code: `${this.name}_bind_platform`,
      permissions: [`admin_permission_tree_get`, `${this.name}_BindPlatformPermissions_get`, `${this.name}_BindPlatformPermissions_post`]
    }
  }
})()

/** 路由信息 */
export const route = {
  page,
  component: () => import(/* webpackChunkName: "admin.role" */ './index')
}

export default page
