/** 页面信息 */
const page = new function () {
  this.title = '审计日志'
  this.name = 'admin_auditinfo'
  this.path = '/admin/auditinfo'
}()

/** 路由信息 */
export const route = {
  page,
  component: () => import(/* webpackChunkName: "admin.auditinfo" */ './index')
}

export default page
