/** 页面信息 */
const page = new function() {
  this.title = '首页'
  this.name = 'admin_home'
  this.path = '/admin/home'
  this.buttons = {
    details: {
      text: '首页',
      code: `${this.name}_home`
    }
  }
}()

/** 路由信息 */
export const route = {
  page,
  component: () => import(/* webpackChunkName: "admin.home" */ './index')
}

export default page
