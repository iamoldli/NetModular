import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

const appStore = {
  namespaced: true,
  modules: {}
}

const requireComponent = require.context('./modules', true, /\.js$/)
requireComponent.keys().map(fileName => {
  const key = fileName.replace('./', '').replace('.js', '')
  appStore.modules[key] = requireComponent(fileName).default
})

var store
export default stores => {
  store = new Vuex.Store(stores)
  store.registerModule('app', appStore)
  return store
}
export { store }
