export default [
  {
    // 默认页
    path: '/default',
    name: 'Default',
    component: () =>
      import(/* webpackChunkName: "default" */ '../page/default/'),
    meta: {
      title: '默认页',
      cache: false
    }
  },
  {
    // 内嵌页面
    path: '/iframe',
    name: 'iframe',
    component: () => import(/* webpackChunkName: "frame" */ '../page/iframe/')
  },
  {
    // 403
    path: '/error/403',
    name: 'Error403',
    component: () => import(/* webpackChunkName: "error_403" */ '../page/403'),
    meta: {
      title: 'Error 403',
      cache: false
    }
  },
  {
    // 404
    path: '/error/404',
    name: 'Error404',
    component: () => import(/* webpackChunkName: "error_404" */ '../page/404'),
    meta: {
      title: 'Error 404',
      cache: false
    }
  },
  { path: '*', redirect: '/error/404' }
]
