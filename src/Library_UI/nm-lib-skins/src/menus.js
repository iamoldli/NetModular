let id = 1
const resolve = (name, title, icon) => {
  id++
  return {
    id,
    type: 1,
    name: name + '(nm-' + title + ')',
    routeName: 'nm-' + title,
    icon: icon || 'list',
    iconColor: '',
    url: '',
    level: 2,
    show: true,
    sort: 0
  }
}
export default [
  {
    id: '1',
    type: 0,
    name: '组件',
    routeName: '',
    icon: 'app',
    iconColor: '#67C23A',
    url: '',
    level: 1,
    show: true,
    sort: 0,
    children: [
      resolve('容器', 'container'),
      resolve('盒子', 'box'),
      resolve('按钮', 'button'),
      resolve('对话框', 'dialog', 'dialog'),
      resolve('基础表单', 'form', 'edit'),
      resolve('表单对话框', 'form-dialog', 'edit'),
      resolve('表单页', 'form-page', 'edit'),
      resolve('列表页', 'list', 'list'),
      resolve('抽屉', 'drawer', 'list'),
      resolve('分割线', 'divider', 'list')
    ]
  }
]
