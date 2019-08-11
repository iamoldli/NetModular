/** 页面信息 */
const page = new function() {
  this.title = '任务列表'
  this.name = 'Quartz_Job'
  this.path = '/Quartz/Job'

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
    pause: {
      text: '暂停',
      type: 'text',
      icon: 'pause',
      code: `${this.name}_pause`,
      permissions: [`${this.name}_pause_post`]
    },
    resume: {
      text: '启动',
      type: 'text',
      icon: 'run',
      code: `${this.name}_resume`,
      permissions: [`${this.name}_resume_post`]
    },
    log: {
      text: '日志',
      type: 'text',
      icon: 'log',
      code: `${this.name}_log`,
      permissions: [`${this.name}_log_get`]
    },
    del: {
      text: '删除',
      type: 'text',
      icon: 'delete',
      code: `${this.name}_del`,
      permissions: [`${this.name}_delete_delete`]
    }
  }
}()

/** 路由信息 */
export const route = {
  page,
  component: () => import(/* webpackChunkName: "Quartz.Job" */ './index')
}

export default page
