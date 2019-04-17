// 对话框菜单

const defaultOptions = {
  title: '',
  url: '',
  width: '60%',
  height: '70%',
  noScrollbar: true,
  fullscreen: true
}

export default {
  namespaced: true,
  state: {
    visible: false,
    title: '',
    url: '',
    width: '60%',
    height: '70%',
    noScrollbar: true,
    fullscreen: true,
    noFooter: true
  },
  actions: {
    /**
     * @description 打开
     */
    open({ commit }, options) {
      commit('set', options)
    },
    /**
     * @description 打开
     */
    close({ commit }, options) {
      commit('reset', options)
    }
  },
  mutations: {
    set(state, options) {
      Object.assign(state, options)
      state.visible = true
    },
    reset(state) {
      Object.assign(state, defaultOptions)
      state.visible = false
    }
  }
}
