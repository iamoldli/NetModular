/** 页面信息 */
const page = new function() {
  this.title = '模块管理'
  this.name = 'admin_moduleinfo'
  this.path = '/admin/moduleinfo'
  this.buttons = {
    sync: {
      text: '同步',
      code: `${this.name}_sync`,
      icon: 'refresh'
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
  component: () => import(/* webpackChunkName: "admin.moduleinfo" */ './index')
}

export default page
