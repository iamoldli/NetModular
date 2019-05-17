/** 页面信息 */
const page = new function() {
  this.title = '分类列表'
  this.name = 'Blog_Category'
  this.path = '/Blog/Category'
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
  component: () => import(/* webpackChunkName: "Blog.Category" */ './index')
}

export default page
