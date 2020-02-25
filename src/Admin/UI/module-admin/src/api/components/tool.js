import module from '../../module'

export default name => {
  const root = `${module.code}/${name}/`
  const crud = $http.crud(root)

  const urls = {
    enumSelect: root + 'EnumSelect'
  }

  const enumSelect = params => {
    return $http.get(urls.enumSelect, params)
  }

  return {
    ...crud,
    enumSelect
  }
}
