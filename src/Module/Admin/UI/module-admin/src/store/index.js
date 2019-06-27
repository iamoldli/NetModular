import token from './modules/token'
export default {
  namespaced: true,
  state: {
    // 当前操作的菜单
    currentMenu: {}
  },
  mutations: {
    // 设置当前操作的菜单
    setCurrentMenu (state, menu) {
      state.currentMenu = menu
    }
  },
  modules: {
    token
  }
}
