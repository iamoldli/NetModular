/** 页面信息 */
const page = new function() {
  this.title = '用户信息列表'
  this.icon = 'user'
  this.name = 'PersonnelFiles_User'
  this.path = '/PersonnelFiles/User'

  // 关联权限
  this.permissions = [`${this.name}_query_get`]

  // 按钮
  this.buttons = {
    add: {
      text: '添加',
      type: 'success',
      icon: 'add',
      code: `${this.name}_add`,
      permissions: [`${this.name}_add_post`, `${this.name}_UploadPicture_post`]
    },
    edit: {
      text: '编辑',
      type: 'text',
      icon: 'edit',
      code: `${this.name}_edit`,
      permissions: [
        `${this.name}_edit_get`,
        `${this.name}_update_post`,
        `${this.name}_UploadPicture_post`,
        `${this.name}_EditContact_get`,
        `${this.name}_UpdateContact_post`,
        `${this.name}_ContactDetails_get`
      ]
    },
    del: {
      text: '删除',
      type: 'text',
      icon: 'delete',
      code: `${this.name}_del`,
      permissions: [`${this.name}_delete_delete`]
    },
    workHistory: {
      text: '工作经历',
      type: 'text',
      icon: 'work',
      code: `${this.name}_work_history`,
      permissions: [
        'PersonnelFiles_UserWorkHistory_query_get',
        'PersonnelFiles_UserWorkHistory_add_post',
        'PersonnelFiles_UserWorkHistory_edit_get',
        'PersonnelFiles_UserWorkHistory_update_get',
        'PersonnelFiles_UserWorkHistory_delete_delete'
      ]
    },
    educationHistory: {
      text: '教育经历',
      type: 'text',
      icon: 'education',
      code: `${this.name}_education_history`,
      permissions: [
        'PersonnelFiles_UserEducationHistory_query_get',
        'PersonnelFiles_UserEducationHistory_add_post',
        'PersonnelFiles_UserEducationHistory_edit_get',
        'PersonnelFiles_UserEducationHistory_update_get',
        'PersonnelFiles_UserEducationHistory_delete_delete'
      ]
    }
  }
}()

/** 路由信息 */
export const route = {
  page,
  component: () =>
    import(/* webpackChunkName: "PersonnelFiles.User" */ './index')
}

export default page
