/** 页面信息 */
const page = new function() {
  this.title = '系统配置'
  this.name = 'admin_system_config'
  this.path = '/admin/system/config'
}()

/** 路由信息 */
export const route = {
  page,
  component: () => import(/* webpackChunkName: "admin.menu" */ './index')
}

export default page
