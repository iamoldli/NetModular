/** 页面信息 */
const page = new function () {
  this.title = '系统配置'
  this.name = 'admin_config_system'
  this.path = '/admin/config/system'
}()

/** 路由信息 */
export const route = {
  page,
  component: () => import(/* webpackChunkName: "admin.menu" */ './index')
}

export default page
