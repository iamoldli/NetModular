import module from '../../module'

export default name => {
  const root = `${module.code}/${name}/`

  const urls = {
    enumSelect: root + 'EnumSelect',
    platformSelect: root + 'PlatformSelect',
    loginModeSelect: root + 'LoginModeSelect'
  }

  const enumSelect = params => {
    return $http.get(urls.enumSelect, params)
  }

  const platformSelect = () => {
    return $http.get(urls.platformSelect)
  }

  const loginModeSelect = () => {
    return $http.get(urls.loginModeSelect)
  }

  return {
    enumSelect,
    platformSelect,
    loginModeSelect
  }
}
