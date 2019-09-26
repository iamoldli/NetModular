// 皮肤
export default {
  namespaced: true,
  state: {
    initialized: false,
    /** 当前皮肤 */
    current: {
      /** 名称 */
      name: 'pretty',
      /** 主题 */
      theme: '',
      /** 字号 */
      fontSize: 'small'
    },
    /** 皮肤列表 */
    list: []
  },
  getters: {
    fontSize(state) {
      return state.current.fontSize
    }
  },
  actions: {
    /**
     * @description 初始化皮肤信息
     * @param {*} skin
     */
    init({ commit, dispatch }, skin) {
      if (skin) {
        commit('init', skin)
      }
    }
  },
  mutations: {
    /**
     * @description 初始化
     * @param {Object} state 状态
     * @param {Object} skin 皮肤
     */
    init(state, skin) {
      state.current = skin
      if (state.current.skin === '') {
        state.current.skin = 'pretty'
      }
      if (skin.theme === '') {
        state.current.theme = 'default'
      }
      if (skin.fontSize === 'default') {
        state.current.fontSize = ''
      }

      /** 设置body的类 */
      document.body.className = `nm-skin-${state.current.name} theme-${state.current.theme} font-size-${state.current.fontSize}`
      state.initialized = true
    },
    /**
     * @description 注册皮肤
     */
    useSkin(state, skin) {
      state.list.push(skin)
    },
    /**
     * @description 皮肤切换
     * @param {Object} state 状态
     */
    toggle(state, skin) {
      state.current = Object.assign({}, skin)
    }
  }
}
