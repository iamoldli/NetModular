/** 页面信息 */
const page = new function() {
  this.title = '菜单管理'
  this.name = 'admin_menu'
  this.path = '/admin/menu'
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
    bindPermission: {
      text: '绑定权限',
      code: `${this.name}_bind_permission`
    },
    bindButton: {
      text: '绑定按钮',
      code: `${this.name}_bind_button`
    },
    sort: {
      text: '排序',
      code: `${this.name}_sort`
    }
  }
}()

/** 路由信息 */
export const route = {
  page,
  component: () => import(/* webpackChunkName: "admin.menu" */ './index')
}

export default page
