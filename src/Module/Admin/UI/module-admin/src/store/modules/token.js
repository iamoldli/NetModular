import token from '../../extensions/token'
export default {
  namespaced: true,
  state: {
    /** 请求令牌 */
    accessToken: '',
    /** 刷新令牌 */
    refreshToken: ''
  },
  actions: {
    load ({ commit }) {
      const t = token.get()
      if (t) {
        commit('init', t)
      }
    },
    init ({ commit }, token) {
      commit('init', token)
    }
  },
  mutations: {
    init (state, token) {
      Object.assign(state, token)
    }
  }
}
