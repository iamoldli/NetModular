/** 侧边栏状态 */
export default {
  namespaced: true,
  state: {
    collapse: false
  },
  actions: {
    /**
     * @description 切换折叠状态
     */
    toggle ({ state, commit }) {
      commit('collapseSet', !state.collapse)
    },
    /**
     * @description 隐藏
     */
    hide ({ commit }) {
      commit('collapseSet', false)
    },
    /**
     * @description 显示
     */
    show ({ commit }) {
      commit('collapseSet', true)
    }
  },
  mutations: {
    collapseSet (state, collapse) {
      state.collapse = collapse
    }
  }
}
