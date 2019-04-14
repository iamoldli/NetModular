import { http } from 'nm-lib-utils'
import routerConfig from '../router'
import { Message } from 'element-ui'
import token from '../extensions/token'
import { store } from 'nm-lib-skins'

// 消息提醒显示时长(ms)
const messageDuration = 1500

export default baseUrl => {
  // 初始化请求
  http.init(axios => {
    // 接口根路径
    axios.defaults.baseURL = baseUrl

    // 拦截请求
    axios.interceptors.request.use(
      function (config) {
        let t = token.get()
        if (t && t.accessToken) {
          config.headers.Authorization = 'Bearer ' + t.accessToken
        }
        return config
      },
      function (error) {
        return Promise.reject(error)
      }
    )

    // 响应前拦截器
    axios.interceptors.response.use(
      response => {
        if (response.data.code === 1) {
          return response.data.data
        } else {
          Message.error({
            message: response.data.msg,
            showClose: true,
            center: true,
            duration: messageDuration
          })
          return Promise.reject(response.data.msg)
        }
      },
      error => {
        const $router = routerConfig.$router
        let currentRoute = $router.currentRoute
        let redirect =
          currentRoute.name !== 'Login' ? currentRoute.fullPath : '/' // 跳转页面

        if (error && error.response) {
          switch (error.response.status) {
            case 401:
              // 删除token
              token.remove()
              $router.push({
                name: 'Login',
                query: {
                  redirect
                }
              })
              break
            case 403:
              console.log(1)
              store.dispatch(
                'app/page/close',
                {
                  fullPath: currentRoute.path,
                  router: $router,
                  to: {
                    name: 'Error403'
                  }
                },
                { root: true }
              )
              break
            default:
              Message.error({
                message: '系统异常，请联系管理员~',
                duration: messageDuration
              })
              break
          }
        } else {
          if (currentRoute.path === '/login') {
            Message.error({
              message: '无法连接网络~',
              duration: messageDuration
            })
          } else {
            token.remove()
            $router.push({ name: 'Login', query: { redirect } })
          }
        }
        return Promise.reject(error)
      }
    )
  })
}
