import sidebar from '../../pretty/store/modules/sidebar'
export default {
  namespaced: true,
  state: {
    menus: []
  },
  mutations: {
    setMenus(state, menus) {
      state.menus = menus
    }
  },
  modules: {
    sidebar
  }
}
