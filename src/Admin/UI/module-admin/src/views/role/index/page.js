/** 页面信息 */
const page = new function() {
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
      type: 'success',
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
    bindMenu: {
      text: '绑定菜单',
      type: 'text',
      icon: 'bind',
      code: `${this.name}_bind_menu`,
      permissions: [
        `${this.name}_menulist_get`,
        `${this.name}_bindmenu_post`,
        `${this.name}_menubuttonlist_get`,
        `${this.name}_bindmenubutton_post`
      ]
    }
  }
}()

/** 路由信息 */
export const route = {
  page,
  component: () => import(/* webpackChunkName: "admin.role" */ './index')
}

export default page
