/** 页面信息 */
const page = new (function() {
  this.title = '岗位列表'
  this.icon = 'post'
  this.name = 'personnelfiles_position'
  this.path = '/personnelfiles/position'
  this.buttons = {
    del: {
      text: '删除',
      type: 'text',
      icon: 'delete',
      code: `${this.name}_del`,
      permissions: [`${this.name}_delete_delete`]
    }
  }
})()

/** 路由信息 */
export const route = {
  page,
  component: () =>
    import(/* webpackChunkName: "PersonnelFiles.Position" */ './index')
}

export default page
