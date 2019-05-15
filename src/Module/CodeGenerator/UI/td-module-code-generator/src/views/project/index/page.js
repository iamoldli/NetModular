/** 页面信息 */
const page = new function() {
  this.title = '项目列表'
  this.name = 'codegenerator_project'
  this.path = '/codegenerator/project'
  this.buttons = {
    add: {
      text: '添加',
      code: `${this.name}_add`,
      icon: 'add'
    },
    edit: {
      text: '编辑',
      code: `${this.name}_edit`
    },
    del: {
      text: '删除',
      code: `${this.name}_del`
    },
    buildCode: {
      text: '生成',
      code: `${this.name}_build_code`
    }
  }
}()

/** 路由信息 */
export const route = {
  page,
  component: () => import(/* webpackChunkName: "admin.moduleinfo" */ './index')
}

export default page
