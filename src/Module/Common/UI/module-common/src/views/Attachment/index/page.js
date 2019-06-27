/** 页面信息 */
const page = new function() {
  this.title = '附件管理'
  this.name = 'Common_Attachment'
  this.path = '/Common/Attachment'
  this.buttons = {
    del: {
      text: '删除',
      code: `${this.name}_del`
    },
    export: {
      text: '导出',
      icon: 'export',
      code: `${this.name}_export`
    }
  }
}()

/** 路由信息 */
export const route = {
  page,
  component: () => import(/* webpackChunkName: "Common.Attachment" */ './index')
}

export default page
