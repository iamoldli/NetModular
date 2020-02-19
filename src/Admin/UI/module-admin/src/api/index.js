import module from '../module'

// 注册接口列表为window全局属性
window.$api = {}

let apis = {}
const requireComponent = require.context('./components', true, /\.*\.js$/)
requireComponent.keys().map(fileName => {
  const name = fileName.replace('./', '').replace('.js', '')
  const func = requireComponent(fileName).default
  apis[name] = func(name)
})
$api[module.code] = apis
