const isDev = process.env.NODE_ENV !== 'production'

const config = {
  baseUrl: 'http://localhost:6226/api/'
}

// 开发模式
if (isDev) {
  config.baseUrl = 'http://localhost:6226/api/'
}
export default config
