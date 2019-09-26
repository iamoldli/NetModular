// 打开路由菜单
const openRoute = (router, menu) => {
  let route = { name: menu.routeName, params: {} }
  if (menu.routeQuery) {
    route.query = JSON.parse(menu.routeQuery)
  }
  if (menu.routeParams) {
    route.params = JSON.parse(menu.routeParams)
  }

  route.params['tn_'] = menu.name

  router.push(route)
}

// 打开链接菜单
const openLink = (router, menu) => {
  const target = menu.target
  if (!target || target === 0) {
    window.open(menu.url, '_blank')
  } else if (target === 1) {
    window.open(menu.url, '_self')
  } else if (target === 2) {
    // Dialog模式
    this.dialogMenuOpen({
      title: menu.name,
      icon: menu.icon,
      url: menu.url,
      width: menu.dialogWidth,
      height: menu.dialogHeight,
      fullscreen: menu.dialogFullscreen
    })
  } else if (target === 3 || target === 4) {
    // 当前皮肤下，容器内和内容区一样
    router.push({
      name: 'iframe',
      params: { url: encodeURI(menu.url), tn_: menu.name }
    })
  }
}

const open = (router, menu) => {
  // 1、站内路由 2、站外链接
  if (menu.type === 1) {
    openRoute(router, menu)
  } else if (menu.type === 2) {
    openLink(router, menu)
  }
}

export { open }
