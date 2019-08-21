/** 页面信息 */
const page = new function() {
  this.title = '系统配置'
  this.icon = 'config'
  this.name = 'admin_system'
  this.path = '/admin/system/config'

  // 关联权限
  this.permissions = [
    `${this.name}_config_post`,
    `${this.name}_UploadLogo_post`
  ]
}()

/** 路由信息 */
export const route = {
  page,
  component: () => import(/* webpackChunkName: "admin.menu" */ './index')
}

export default page
