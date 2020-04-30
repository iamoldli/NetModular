/** 页面信息 */
const page = new (function() {
  this.title = '配置中心'
  this.icon = 'tools'
  this.name = 'admin_config'
  this.path = '/admin/config'

  // 关联权限
  this.permissions = [`${this.name}_edit_get`, `${this.name}_update_post`, `${this.name}_UploadLogo_post`, `${this.name}_LogoUrl_get`, `${this.name}_Descriptors_get`]

  // 按钮
  this.buttons = {}
})()

/** 路由信息 */
export const route = {
  page,
  component: () => import(/* webpackChunkName: "admin.config" */ './index')
}

export default page
