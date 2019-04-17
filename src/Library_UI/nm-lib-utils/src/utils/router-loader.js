/** 面包屑 */
let breadcrumb = [
  {
    title: '首页',
    path: '/'
  }
]

// 解析面包屑数组
const resolveBreadcrumb = (page, parent) => {
  // 如果是子路由，则先继承父级路由的面包屑数组
  let bc = parent ? [...parent.meta.breadcrumb] : [...breadcrumb]

  if (page.meta.breadcrumb) {
    // 自定义
    bc.concat(page.breadcrumb)
  } else if (page.meta.title) {
    // 根据title设置
    bc.push({ title: page.meta.title, path: '' })
  }
  page.meta.breadcrumb = bc
}

// 递归解析嵌套路由
const resolveNestedRoute = (page, pages, parent) => {
  resolveBreadcrumb(page, parent)
  page.children = []
  pages.map(p => {
    if (p.parent === page.name) {
      page.children.push(resolveNestedRoute(p, pages, page))
    }
  })
  delete page.parent
  return page
}

/**
 * 单个页面配置信息转为路由信息
 * @param {Object} config 配置信息
 */
export const page2Router = config => {
  const { page, component } = config
  return {
    path: page.path,
    name: page.name,
    parent: page.parent,
    component: component,
    props: true,
    meta: {
      title: page.title,
      frameIn: page.frameIn,
      cache: page.cache,
      breadcrumb: page.breadcrumb,
      buttons: page.buttons
    }
  }
}

/**
 * @description 页面数组转为路由数组
 * @param {Object} module 模块信息
 * @param {Object} pages 页面数组
 */
export const pages2Routes = (module, pages) => {
  // 添加模块根面包屑
  breadcrumb.push({
    title: module.name,
    path: ''
  })

  const routes = pages
    .filter(p => !p.parent)
    .map(page => resolveNestedRoute(page, pages))
  return routes
}

export default {
  page2Router,
  pages2Routes
}
