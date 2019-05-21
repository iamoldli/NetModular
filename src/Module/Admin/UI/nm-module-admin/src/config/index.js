const isDev = process.env.NODE_ENV !== 'production'

const config = {
  baseUrl: '/api/'
}

// 开发模式
if (isDev) {
  config.baseUrl = 'http://118.24.75.170:6220/api/'
}

export default config
