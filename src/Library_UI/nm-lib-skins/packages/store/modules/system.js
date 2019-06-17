import _ from 'lodash'
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
      skin: true,
      // 退出
      logout: true,
      // 用户信息
      userInfo: true
    },
    /**
     * 是否启用按钮权限
     */
    buttonPermission: true,
    /**
     * 菜单只能打开一个
     */
    menuUniqueOpened: true,
    /**
     * 用户信息页(路由名称)
     */
    userInfoPage: 'UserInfo',
    /**
     * 设置对话框是否可以点击模态框关闭
     */
    dialogCloseOnClickModal: false,
    /// //////////////////////////////////////////////////////////
    /** 上面的属性是可在管理平台配置的， 下面的属性都是系统代码内去设置的 */
    /// //////////////////////////////////////////////////////////
    /** 退出方法 */
    logout: null,
    /** 修改密码方法 */
    updatePassword: null,
    /** 查询登录信息方法 */
    getLoginInfo: null,
    /** 模块列表 */
    modules: []
  },
  actions: {
    async init({ commit, dispatch }, { system, router }) {
      commit('init', { system })
      // 配置页面信息
      await dispatch('app/page/load', null, { root: true })
    },
    /**
     * @description 退出
     */
    async logout({ state, dispatch }) {
      // 退出事件
      await state.logout()

      // 账号退出
      dispatch('app/account/logout', null, { root: true })
    }
  },
  mutations: {
    init(state, { system }) {
      _.merge(state, system)
      state.logo = state.logoUrl
      if (!state.logo) {
        state.logo = './images/logo.png'
      }
      if (!state.userInfoPage) {
        state.userInfoPage = 'UserInfo'
      }
    }
  }
}
