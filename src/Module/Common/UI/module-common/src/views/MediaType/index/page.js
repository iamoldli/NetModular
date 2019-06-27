/** 页面信息 */
const page = new function() {
  this.title = '多媒体管理'
  this.name = 'Common_MediaType'
  this.path = '/Common/MediaType'
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
    }
  }
}()

/** 路由信息 */
export const route = {
  page,
  component: () => import(/* webpackChunkName: "Common.MediaType" */ './index')
}

export default page
