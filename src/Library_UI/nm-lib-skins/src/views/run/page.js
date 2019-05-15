/** 页面信息 */
const page = new function () {
  this.title = '代码预览'
  this.name = 'RunCode'
  this.path = '/run'
  this.frameIn = false
  this.cache = false
}()

export const route = {
  page,
  component: () => import('./index')
}

export default page
