/** 页面信息 */
const page = new (function() {
  this.title = '配置管理'
  this.icon = 'tag'
  this.name = 'admin_config'
  this.path = '/admin/config'

  // 关联权限
  this.permissions = [
    `${this.name}_query_get`,
    `${this.name}_add_post`,
    `${this.name}_edit_get`,
    `${this.name}_update_post`,
    `${this.name}_delete_delete`,
    /**模块配置 */
    'admin_module_optionsedit_get',
    'admin_module_optionsupdate_post',
    /**系统配置*/
    `admin_system_UpdateBaseConfig_post`,
    `admin_system_UpdateComponentConfig_post`,
    `admin_system_UpdateLoginConfig_post`,
    `admin_system_UpdatePermissionConfig_post`,
    `admin_system_UpdateToolbarConfig_post`,
    `admin_system_UpdatePathConfig_post`,
    `admin_system_UploadLogo_post`
  ]

  // 按钮
  this.buttons = {}
})()

/** 路由信息 */
export const route = {
  page,
  component: () => import(/* webpackChunkName: "admin.config" */ './index')
}

export default page
