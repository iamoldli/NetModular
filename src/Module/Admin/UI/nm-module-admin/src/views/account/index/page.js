/** 页面信息 */
const page = new function () {
  this.title = '账户管理'
  this.name = 'admin_account'
  this.path = '/admin/account'
  this.buttons = {
    add: {
      text: '添加',
      code: `${this.name}_add`,
      icon: 'add'
    },
    edit: {
      text: '编辑',
      code: `${this.name}_edit`
    },
    del: {
      text: '删除',
      code: `${this.name}_del`
    },
    resetPassword: {
      text: '重置密码',
      code: `${this.name}_reset_password`
    }
  }
}()

/** 路由信息 */
export const route = {
  page,
  component: () => import(/* webpackChunkName: "admin.account" */ './index')
}

export default page
