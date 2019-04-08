const isDev = process.env.NODE_ENV !== 'production'

const config = {
  baseUrl: 'http://10.32.1.183:6220/api/'
}

// 开发模式
if (isDev) {
  // config.baseUrl = 'http://10.32.1.183:6220/api/'
  config.baseUrl = 'http://localhost:6220/api/'
}

export default config
