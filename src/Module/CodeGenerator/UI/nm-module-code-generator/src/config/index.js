const isDev = process.env.NODE_ENV !== 'production'

const config = {
  baseUrl: 'http://localhost:6221/api/'
}

// 开发模式
if (isDev) {
  config.baseUrl = 'http://localhost:6220/api/'
}
export default config
