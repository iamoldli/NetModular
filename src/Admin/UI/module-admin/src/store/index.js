export default {
  namespaced: true,
  state: {
    // 登录配置
    loginSettings: {
      // 登录页类型
      type: 'default',
      // 登录账户类型选项
      accountTypeOptions: null
    },
    // 当前操作的菜单
    currentMenu: {}
  },
  actions: {
    // 设置登录配置
    setLoginSettings({ commit }, settings) {
      commit('setLoginSettings', settings)
    }
  },
  mutations: {
    // 设置当前操作的菜单
    setCurrentMenu(state, menu) {
      state.currentMenu = menu
    },
    setLoginSettings(state, settings) {
      Object.assign(state.loginSettings, settings)
    }
  }
}
