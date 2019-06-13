/** 页面信息 */
const page = new function() {
  this.title = '部门列表'
  this.name = 'PersonnelFiles_Department'
  this.path = '/PersonnelFiles/Department'
  this.buttons = {
    add: {
      text: '添加',
      code: `${this.name}_add`,
      icon: 'add'
    },
    edit: {
      text: '编辑',
      icon: 'edit',
      code: `${this.name}_edit`
    },
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
  component: () => import(/* webpackChunkName: "PersonnelFiles.Department" */ './index')
}

export default page
