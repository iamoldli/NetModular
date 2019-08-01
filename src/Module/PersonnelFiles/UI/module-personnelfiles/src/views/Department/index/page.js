/** 页面信息 */
const page = new function() {
  this.title = '部门列表'
  this.icon = 'department'
  this.name = 'PersonnelFiles_Department'
  this.path = '/PersonnelFiles/Department'

  // 关联权限
  this.permissions = [`${this.name}_query_get`]

  // 按钮
  this.buttons = {
    add: {
      text: '添加',
      type: 'success',
      icon: 'add',
      code: `${this.name}_add`,
      permissions: [`${this.name}_add_post`]
    },
    edit: {
      text: '编辑',
      type: 'text',
      icon: 'edit',
      code: `${this.name}_edit`,
      permissions: [`${this.name}_edit_get`, `${this.name}_update_post`]
    },
    del: {
      text: '删除',
      type: 'text',
      icon: 'delete',
      code: `${this.name}_del`,
      permissions: [`${this.name}_delete_delete`]
    },
    position: {
      text: '岗位',
      type: 'text',
      icon: 'edit',
      code: `${this.name}_position`,
      permissions: [`PersonnelFiles_position_query_get`]
    },
    positionAdd: {
      text: '岗位添加',
      type: 'success',
      icon: 'add',
      code: `${this.name}_position_add`,
      permissions: [`PersonnelFiles_position_add_post`]
    },
    positionEdit: {
      text: '岗位编辑',
      type: 'text',
      icon: 'edit',
      code: `${this.name}_position_edit`,
      permissions: [
        `PersonnelFiles_position_edit_get`,
        'PersonnelFiles_position_update_post'
      ]
    },
    positionDel: {
      text: '岗位删除',
      type: 'text',
      icon: 'delete',
      code: `${this.name}_position_del`,
      permissions: [`PersonnelFiles_position_delete_delete`]
    }
  }
}()

/** 路由信息 */
export const route = {
  page,
  component: () =>
    import(/* webpackChunkName: "PersonnelFiles.Department" */ './index')
}

export default page
