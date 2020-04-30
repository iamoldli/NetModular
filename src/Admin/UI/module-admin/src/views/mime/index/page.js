/** 页面信息 */
const page = new (function() {
  this.title = 'MIME'
  this.icon = 'media'
  this.name = 'admin_mime'
  this.path = '/admin/mime'

  // 关联权限
  this.permissions = [`${this.name}_query_get`]

  this.buttons = {
    add: {
      text: '添加',
      type: 'success',
      code: `${this.name}_add`,
      icon: 'add',
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
    }
  }
})()

/** 路由信息 */
export const route = {
  page,
  component: () => import(/* webpackChunkName: "Admin.Mime" */ './index')
}

export default page
