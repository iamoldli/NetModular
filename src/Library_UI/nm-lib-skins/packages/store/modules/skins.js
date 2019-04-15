import pretty from '../../skins/pretty/store'
import classics from '../../skins/classics/store'
function storeLocalStore (state) {
  window.localStorage.setItem('skins', JSON.stringify(state))
}
function getStoreLocalStore () {
  window.localStorage.getItem('skins')
}
// 皮肤
export default {
  namespaced: true,
  state: {
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
    fontSize (state) {
      return state.current.fontSize
    }
  },
  actions: {
    /**
     * @description 初始化皮肤信息
     * @param {*} skin
     */
    init ({ commit }, skin) {
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
    init (state, skin) {
      let oldSkin = getStoreLocalStore()
      if (oldSkin != null) {
        state.current = JSON.parse(oldSkin)
      }
      if (skin.fontSize === '') {
        state.current.fontSize = 'small'
      }
    },
    /**
     * @description 皮肤切换
     * @param {Object} state 状态
     */
    toggle (state, name) {
      state.name = name
    },
    themeChange (state, name) {
      state.current.theme = name.theme
      storeLocalStore(state.current)
    }
  },
  modules: {
    pretty,
    classics
  }
}
