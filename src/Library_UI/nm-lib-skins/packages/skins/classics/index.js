import store from './store'
import component from './index.vue'
import preview from './preview.png'

export default {
  component,
  store,
  skin: {
    name: '经典',
    code: 'classics',
    preview,
    themes: [
      {
        name: 'default',
        color: '#03a9f4'
      },
      {
        name: 'blue-light',
        color: '#e4e7ed'
      }
    ]
  }
}
