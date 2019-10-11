/** 页面信息 */
const page = new function() {
  this.title = '多媒体管理'
  this.name = 'common_mediatype'
  this.path = '/common/mediatype'

  // 关联权限
  this.permissions = [`${this.name}_query_get`]

  this.buttons = {
    add: {
      text: '添加',
      type: 'success',
      code: `${this.name}_add`,
      icon: 'add',
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
    }
  }
}()

/** 路由信息 */
export const route = {
  page,
  component: () => import(/* webpackChunkName: "Common.MediaType" */ './index')
}

export default page
