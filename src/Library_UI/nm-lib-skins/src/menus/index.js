import componentMenus from '../views/components/menus'
let menus = []
// 基础组件
menus.push(componentMenus)

const setId = (menus, parentId) => {
  menus.map((m, i) => {
    if (parentId) {
      m.id = parentId + '-' + i
    } else {
      m.id = i + ''
    }
    if (m.children) {
      setId(m.children, m.id)
    }
  })
}
setId(menus)
export default menus
