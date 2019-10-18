import module from '../../module'

export default name => {
  const root = `${module.code}/${name}/`
  const crud = $http.crud(root)
  const urls = {
    getValue: root + 'GetValue'
  }

  const getValue = key => {
    return $http.get(urls.getValue, { key })
  }

  return {
    ...crud,
    getValue
  }
}
