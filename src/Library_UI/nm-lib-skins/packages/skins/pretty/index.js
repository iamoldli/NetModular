import store from './store'
import component from './index.vue'
import preview from './preview.png'

export default {
  component,
  store,
  skin: {
    name: '靓丽',
    code: 'pretty',
    preview,
    themes: [
      {
        name: 'default',
        color: '#03a9f4'
      },
      {
        name: 'blue-light',
        color: '#e4e7ed'
      },
      {
        name: 'green',
        color: '#18ce0f'
      },
      {
        name: 'green-light',
        color: '#e4e7ed'
      }
    ]
  }
}
