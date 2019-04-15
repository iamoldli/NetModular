import { db } from 'nm-lib-utils'
export default {
  namespaced: true,
  state: {
    /** 账户编号 */
    id: '',
    /** 用户名 */
    name: '',
    /** 头像 */
    avatar: '',
    /** 菜单列表 */
    menus: [],
    /** 权限列表 */
    permissions: [],
    /** 按钮列表 */
    buttons: [],
    /** 皮肤设置 */
    skin: {
      /** 名称 */
      name: 'pretty',
      /** 主题 */
      theme: '',
      /** 字号 medium/small/mini */
      fontSize: ''
    }
  },
  getters: {
    // 当前账户拥有菜单对应的路由列表
    routes: state => {}
  },
  actions: {
    /**
     * @description 初始化
     * @param {*} account 账户信息
     */
    async init({ commit, dispatch }, account) {
      // 设置皮肤
      dispatch('app/skins/init', account.skin, { root: true })

      commit('init', account)

      const accountId = await dispatch('cacheGet')

      // 如果账户变了，则需要清除原有的一些数据
      if (account.id !== accountId) {
        dispatch('app/page/reset', null, { root: true })
      }

      dispatch('cacheSet')
    },
    /**
     * @description 退出
     */
    logout({ commit }) {
      commit('reset')
    },
    cacheGet() {
      return db.get('accountId')
    },
    cacheSet({ state }) {
      db.set('accountId', state.id)
    }
  },
  mutations: {
    init(state, account) {
      Object.assign(state, account)
    },
    reset(state) {
      state.id = ''
      state.name = ''
    }
  }
}
