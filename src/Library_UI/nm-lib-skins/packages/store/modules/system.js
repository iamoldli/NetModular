export default {
  namespaced: true,
  state: {
    /** 标题 */
    title: '',
    /** logo */
    logo: '',
    /** 首页 */
    home: '',
    /** 工具栏 */
    toolbar: {
      // 全屏
      fullscreen: true,
      // 皮肤切换
      skin: true
    },
    // 是否启用按钮权限
    ButtonPermission: true,
    /** 退出方法 */
    logout: null,
    /** 修改密码方法 */
    updatePassword: null,
    /** 模块列表 */
    modules: []
  },
  actions: {
    async init ({ commit, dispatch }, { system, router }) {
      commit('init', { system })
      // 配置页面信息
      await dispatch('app/page/load', null, { root: true })
    },
    /**
     * @description 退出
     */
    async logout ({ state, dispatch }) {
      // 退出事件
      await state.logout()

      // 账号退出
      dispatch('app/account/logout', null, { root: true })
    }
  },
  mutations: {
    init (state, { system }) {
      Object.assign(state, system)
      state.logo = state.logoUrl
      if (!state.logo) {
        state.logo = './images/logo.png'
      }
    }
  }
}
