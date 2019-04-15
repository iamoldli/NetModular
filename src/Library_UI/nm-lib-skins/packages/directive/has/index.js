import { store } from '../../store'

// 按钮权限指令
const has = {
  inserted: (el, binding) => {
    // 验证是否开启按钮验证
    if (store.state.app.system.buttonPermission) {
      var buttons = store.state.app.account.buttons
      const code = binding.value.code
      if (!code || buttons.every(c => c.toLowerCase() !== code.toLowerCase())) {
        el.parentNode.removeChild(el)
      }
    }
  }
}

export default {
  name: 'Has',
  directive: has
}
