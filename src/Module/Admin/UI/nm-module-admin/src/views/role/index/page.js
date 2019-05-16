/** 页面信息 */
const page = new function() {
  this.title = '角色管理'
  this.name = 'admin_role'
  this.path = '/admin/role'
  this.buttons = {
    add: {
      text: '添加',
      code: `${this.name}_add`
    },
    edit: {
      text: '编辑',
      code: `${this.name}_edit`
    },
    del: {
      text: '删除',
      code: `${this.name}_del`
    },
    bindMenu: {
      text: '绑定菜单',
      code: `${this.name}_bind_menu`
    }
  }
}()

/** 路由信息 */
export const route = {
  page,
  component: () => import(/* webpackChunkName: "admin.role" */ './index')
}

export default page
