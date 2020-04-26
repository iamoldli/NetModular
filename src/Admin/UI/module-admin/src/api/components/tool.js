import module from '../../module'

export default name => {
  const root = `${module.code}/${name}/`

  const urls = {
    enumSelect: root + 'EnumSelect',
    platformSelect: root + 'PlatformSelect'
  }

  const enumSelect = params => {
    return $http.get(urls.enumSelect, params)
  }

  const platformSelect = () => {
    return $http.get(urls.platformSelect)
  }

  return {
    enumSelect,
    platformSelect
  }
}
