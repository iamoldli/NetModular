import axios from 'axios'
import qs from 'qs'

function Http () {
  this.axios = axios
}

// 初始化
Http.prototype.init = action => {
  axios.defaults.headers.post['Content-Type'] = 'application/json'
  axios.defaults.headers.put['Content-Type'] = 'application/json'
  action(axios)
}

Http.prototype.post = (url, params) => {
  return axios.post(url, params)
}

Http.prototype.get = (url, params) => {
  return axios.get(url, {
    // 参数
    params,
    // 修改参数序列化方法
    paramsSerializer: p => {
      // 使用逗号分隔参数
      return qs.stringify(p, {
        allowDots: true
      })
    }
  })
}

Http.prototype.delete = (url, params) => {
  return axios.delete(url, {
    // 参数
    params,
    // 修改参数序列化方法
    paramsSerializer: p => {
      // 使用逗号分隔参数
      return qs.stringify(p, {
        allowDots: true
      })
    }
  })
}

Http.prototype.put = (url, params) => {
  return axios.put(url, params)
}

// 通用CRUD接口地址
Http.prototype.crud = root => {
  if (!root.endsWith('/')) {
    root += '/'
  }
  return {
    query (params) {
      return http.get(`${root}query`, params)
    },
    add (params) {
      return http.post(`${root}add`, params)
    },
    remove (id) {
      return http.delete(`${root}delete`, { id })
    },
    edit (id) {
      return http.get(`${root}edit`, { id })
    },
    update (params) {
      return http.post(`${root}update`, params)
    }
  }
}

var http = new Http()

export { http }

export default http
