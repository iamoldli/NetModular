import { db } from 'nm-lib-utils'
import { oneOf } from 'nm-lib-utils/src/utils/assist'

// 默认页路径列表
let defaultPageList = ['/', '/default', '/default/']

/**
 * @description 根据路径查询页面信息以及下标
 * @param {Array} opened vuex
 * @param {String} path 路径
 */
const getPage = (path, opened) => {
  let index = -1
  let page
  opened.every((p, i) => {
    if (p.path === path) {
      index = i
      page = p
      return false
    }
    return true
  })
  return { index, page }
}

/**
 * @description 页面是否缓存
 * @param {Object} page
 */
const isCache = page => {
  return !page.meta || page.meta.cache !== false
}
/**
 * @description 页面是否在框架中显示
 * @param {Object} page
 */
const isFrameIn = page => {
  return !page.meta || page.meta.frameIn !== false
}

export default {
  namespaced: true,
  state: {
    cacheKey: 'tabnav',
    // 已打开的页面
    opened: [],
    // 当前页面信息
    current: {},
    // 缓存的页面
    keepAlive: [],
    // 默认页
    default: ''
  },
  actions: {
    /**
     * @description 从本地数据中读取已打开的页面数据
     * @param {Object} config 配置信息
     * @param {Object} routes 路由列表
     */
    async load({ rootState, commit, dispatch }) {
      let opened = await dispatch('cacheLoad')

      // 初始
      if (opened) {
        commit('init', opened)
        // 缓存刷新
        commit('keepAliveRefresh')
      }

      const sys = rootState.app.system

      // 设置默认页以及当前页为默认页
      let defaultPage = '/'
      // 自定义的默认页添加到默认页路径列表中
      if (sys.home) {
        defaultPage = sys.home.toLowerCase()
        defaultPageList.push(defaultPage)
      }
      // 设置默认页以及当前页为默认页
      commit('defaultSet', defaultPage)
    },
    /**
     * @description 打开页面
     * @param {*} route 路由信息
     */
    async open({ state, commit, dispatch, rootState }, route) {
      // 默认页直接返回
      if (defaultPageList.indexOf(route.path.toLowerCase()) > -1) {
        // 判断是否缓存
        if (isCache(route)) commit('keepAlivePush', route.name)

        commit('currentSet', {
          name: route.name,
          path: state.default,
          isDefault: true
        })
        return
      }

      // 不在框架中显示的页面，直接返回
      if (!isFrameIn(route)) return

      // 加载账户信息，内部会做是否已加载判断
      await dispatch('app/account/init', null, { root: true })

      const page = {
        name: route.name,
        path: decodeURI(route.path),
        fullPath: route.fullPath,
        meta: route.meta,
        query: route.query,
        params: route.params,
        tabName: route.params.tn_ || route.query.tn_ || route.meta.title
      }

      // 内嵌链接
      if (route.name === '_iframe') {
        page.meta = { title: route.query.title }
      }

      // 从路由对应的菜单设置标签名称
      if (!page.tabName) {
        const routeMenu = rootState.app.account.routeMenus.get(route.name)
        if (routeMenu) {
          page.tabName = routeMenu.menu.name
        }
      }
      page.tabName = page.tabName || '未命名'
      // 判断是否已打开
      const notOpend = state.opened.every(p => p.path !== page.path)
      // 如果当前页面未打开, 加入已打开列表
      if (notOpend) {
        // 已打开列表
        commit('add', page)

        // 判断是否缓存
        if (isCache(page)) commit('keepAlivePush', page.name)

        dispatch('cacheInsert')
      }

      commit('currentSet', page)
    },
    /**
     * @description 关闭标签
     * @param {String} path 页面的路径
     * @param {Object} router 路由对象
     */
    async close({ state, commit, dispatch }, { path, router, to }) {
      let newPage = to || '/'
      let { index, page } = getPage(path, state.opened)
      if (index > -1) {
        if (!to) {
          // 如果关闭的页面就是当前显示的页面
          if (state.current.path === page.path) {
            // 打开一个新的页面
            if (index > 0) {
              newPage = state.opened[index - 1]
            }
          }
        }
        commit('remove', index)
        await dispatch('cacheInsert')

        if (isCache(page) === true) {
          commit('keepAliveRemove', page.name)
        }
      }
      router.push(newPage)
    },
    /**
     * @description 关闭左侧标签
     * @param {String} path 选择的页面路径
     * @param {Object} router 路由对象
     */
    closeLeft({ state, commit, dispatch }, { path, router }) {
      path = path || state.current.path
      let { index, page } = getPage(path, state.opened)
      if (index > 0) {
        state.opened.splice(0, index).forEach(item => {
          if (isCache(item) === true) commit('keepAliveRemove', item.name)
        })

        dispatch('cacheInsert')
        // 如果选择的页面不是当前页面，则跳转一下
        if (path !== state.current.path) {
          router.push({
            path: page.path,
            query: page.query,
            params: page.params
          })
        }
      }
    },
    /**
     * @description 关闭右侧标签
     * @param {String} path 选择的页面路径
     * @param {Object} router 路由对象
     */
    closeRight({ state, commit, dispatch }, { path, router }) {
      path = path || state.current.path
      let { index, page } = getPage(path, state.opened)
      if (index < state.opened.length - 1) {
        state.opened.splice(index + 1).forEach(item => {
          if (isCache(item) === true) commit('keepAliveRemove', item.name)
        })
        dispatch('cacheInsert')
        // 如果选择的页面不是当前页面，则跳转一下
        if (path !== state.current) {
          router.push({
            path: page.path,
            query: page.query,
            params: page.params
          })
        }
      }
    },
    /**
     * @description 关闭其它标签
     * @param {String} path 选择的页面路径
     * @param {Object} router 路由对象
     */
    closeOther({ state, commit, dispatch }, { path, router }) {
      path = path || state.current.path
      let { index, page } = getPage(path, state.opened)
      if (index > -1) {
        // 删除右侧
        state.opened.splice(index + 1).forEach(item => {
          if (isCache(item) === true) commit('keepAliveRemove', item.name)
        })
        // 删除左侧
        state.opened.splice(0, index).forEach(item => {
          if (isCache(item) === true) commit('keepAliveRemove', item.name)
        })
        dispatch('cacheInsert')
        // 如果选择的页面不是当前页面，则跳转一下
        if (path !== state.current.path) {
          router.push({
            path: page.path,
            query: page.query,
            params: page.params
          })
        }
      } else {
        dispatch('closeAll')
      }
    },
    /**
     * @description 关闭所有标签
     * @param {String} path 选择的页面路径
     * @param {Object} router 路由对象
     */
    closeAll({ state, commit, dispatch }, { router }) {
      commit('init', [])
      commit('keepAliveClean')
      dispatch('cacheInsert')
      // 跳转到首页
      router.push('/')
    },
    /**
     * @description 缓存插入
     */
    cacheInsert({ state }) {
      db.set(state.cacheKey, state.opened)
    },
    /**
     * @description 缓存加载
     */
    cacheLoad({ state }) {
      return db.get(state.cacheKey)
    },
    /**
     * @description 缓存清除
     */
    cacheClear({ state }) {
      db.set(state.cacheKey, [])
    },
    /**
     * @description 重置状态
     */
    reset({ commit, dispatch }) {
      dispatch('cacheClear')
      commit('reset')
    },
    /** 设置标签名称 */
    setTabName({ commit }, tabName) {
      if (tabName) {
        commit('setTabName', tabName)
      }
    }
  },
  mutations: {
    /**
     * @description 初始化
     * @param {*} opened 已打开的页面
     */
    init(state, opened) {
      state.opened = opened
    },
    /**
     * @description 设置默认页
     * @param {String} url 链接
     */
    defaultSet(state, url) {
      state.default = url
    },
    /**
     * @description 设置当前页
     * @param {String} url 链接
     */
    currentSet(state, current) {
      state.current = current
    },
    /**
     * @description 添加一个页面
     */
    add(state, page) {
      state.opened.push(page)
    },
    /**
     * @description 删除一个页面
     * @param {Number} index 下标
     */
    remove(state, index) {
      state.opened.splice(index, 1)
    },
    /**
     * @description 从已经打开的页面记录中更新需要缓存的页面记录
     * @param {Object} state vuex state
     */
    keepAliveRefresh(state) {
      state.keepAlive = state.opened
        .filter(item => item.meta.cache !== false)
        .map(e => e.name)
    },
    /**
     * @description 删除一个页面的缓存设置
     * @param {Object} state vuex state
     * @param {String} name name
     */
    keepAliveRemove(state, name) {
      const list = [...state.keepAlive]
      const index = list.findIndex(item => item === name)

      if (index > -1) {
        list.splice(index, 1)
        state.keepAlive = list
      }
    },
    /**
     * @description 增加一个页面的缓存设置
     * @param {Object} state vuex state
     * @param {String} name name
     */
    keepAlivePush(state, name) {
      if (!oneOf(state.keepAlive, name)) {
        state.keepAlive.push(name)
      }
    },
    /**
     * @description 清空页面缓存设置
     * @param {Object} state vuex state
     */
    keepAliveClean(state) {
      state.keepAlive = []
    },
    reset(state) {
      state.opened = []
      state.keepAlive = []
      if (state.current && !state.current.isDefault) {
        state.current = {}
      }
    },
    setTabName(state, name) {
      if (!state.current || !state.opened) return

      let page = state.opened.find(p => p.path === state.current.path)
      if (page) {
        page.tabName = name
      }
    }
  }
}
