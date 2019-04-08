/** 页面信息 */
const page = new function () {
  this.title = '权限管理'
  this.name = 'admin_permission'
  this.path = '/admin/permission'
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
    }
  }
}()

/** 路由信息 */
export const route = {
  page,
  component: () => import(/* webpackChunkName: "admin.permission" */ './index')
}

export default page
