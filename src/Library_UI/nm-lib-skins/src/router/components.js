const resolve = (name, title) => {
  return {
    path: '/components/' + name,
    name: 'nm-' + name,
    component: () =>
      import(/* webpackChunkName: "[request]" */ '../views/components/' + name),
    meta: {
      title: title,
      breadcrumb: [
        {
          title: '首页',
          path: '/'
        },
        {
          title: '组件',
          path: '/components'
        }
      ]
    }
  }
}

export default [
  resolve('container', '容器'),
  resolve('box', '盒子'),
  resolve('box-page', '盒子页'),
  resolve('box-group', '盒子分组'),
  resolve('button', '按钮'),
  resolve('dialog', '对话框'),
  resolve('form', '基础表单'),
  resolve('form-dialog', '表单对话框'),
  resolve('form-page', '表单页'),
  resolve('list', '列表页'),
  resolve('drawer', '抽屉'),
  resolve('divider', '分割线')
]
