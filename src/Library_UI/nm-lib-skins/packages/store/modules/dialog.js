export default {
  namespaced: true,
  state: {
    id: 1000
  },
  actions: {
    open ({ state, commit }) {
      commit('setId')
      return state.id
    }
  },
  mutations: {
    setId (state) {
      state.id++
    }
  }
}
