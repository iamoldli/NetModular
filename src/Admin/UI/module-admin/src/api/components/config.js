import module from '../../module'

export default name => {
  const root = `${module.code}/${name}/`
  const crud = $http.crud(root)
  const urls = {
    getValue: root + 'GetValue',
    typeSelect: root + 'TypeSelect'
  }

  const getValue = (key, type, moduleCode) => {
    return $http.get(urls.getValue, { key, type, moduleCode })
  }

  const typeSelect = () => {
    return $http.get(urls.typeSelect)
  }

  return {
    ...crud,
    getValue,
    typeSelect
  }
}
