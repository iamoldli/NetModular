/**
 * 单个页面配置信息转为路由信息
 * @param {Object} config 配置信息
 */
const loadPage = config => {
  const { page, component } = config
  return {
    path: page.path,
    name: page.name.toLowerCase(),
    component: component,
    props: page.props || true,
    meta: {
      title: page.title,
      icon: page.icon,
      frameIn: page.frameIn,
      cache: page.cache,
      buttons: page.buttons,
      permissions: page.permissions
    }
  }
}

/**
 * @description 页面数组转为路由数组
 * @param {Object} module 模块信息
 * @param {Object} pages 页面数组
 */
export default pages => {
  return pages.map(item => loadPage(item))
}
