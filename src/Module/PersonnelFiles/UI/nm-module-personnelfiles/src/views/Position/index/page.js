/** 页面信息 */
const page = new function() {
  this.title = '岗位列表'
  this.name = 'PersonnelFiles_Position'
  this.path = '/PersonnelFiles/Position'
  this.buttons = {
    del: {
      text: '删除',
      icon: 'delete',
      code: `${this.name}_del`
    }
  }
}()

/** 路由信息 */
export const route = {
  page,
  component: () =>
    import(/* webpackChunkName: "PersonnelFiles.Position" */ './index')
}

export default page
