// 加载菜单
export const loadMenu = params => {
  return {
    type: params.type !== 0 ? 1 : 0,
    name: params.name,
    routeName: params.routeName || '',
    icon: params.icon || '',
    iconColor: params.iconColor || '',
    url: params.url || '',
    level: params.level || 3,
    show: params.show || true,
    sort: params.sort || 0,
    children: params.children || []
  }
}
