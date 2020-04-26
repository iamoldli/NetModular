import module from '../../module'

export default name => {
  const root = `${module.code}/${name}/`
  const urls = {
    queryLogin: root + 'Login',
    exportLogin: root + 'LoginExport'
  }

  const queryLogin = params => {
    return $http.get(urls.queryLogin, params)
  }

  const exportLogin = params => {
    return $http.export(urls.exportLogin, params)
  }

  return {
    queryLogin,
    exportLogin
  }
}
