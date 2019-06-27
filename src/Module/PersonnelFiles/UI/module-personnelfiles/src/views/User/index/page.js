/** 页面信息 */
const page = new function() {
  this.title = '用户信息列表'
  this.name = 'PersonnelFiles_User'
  this.path = '/PersonnelFiles/User'
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
    },
    workHistory: {
      text: '工作经历',
      icon: 'work',
      code: `${this.name}_work_history`
    },
    educationHistory: {
      text: '教育经历',
      icon: 'education',
      code: `${this.name}_education_history`
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
