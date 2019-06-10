/** 页面信息 */
const page = new function() {
  this.title = '区划代码列表'
  this.name = 'Common_Area'
  this.path = '/Common/Area'
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
    crawling: {
      text: '爬取',
      icon: 'download',
      code: `${this.name}_crawling`
    }
  }
}()

/** 路由信息 */
export const route = {
  page,
  component: () => import(/* webpackChunkName: "Common.Area" */ './index')
}

export default page
