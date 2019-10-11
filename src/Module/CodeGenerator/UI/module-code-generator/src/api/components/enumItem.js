import module from '../../module'

export default name => {
  const root = `${module.code}/${name}/`
  const crud = $http.crud(root)
  const urls = {
    sort: root + 'sort'
  }
  const querySortList = enumId => {
    return $http.get(urls.sort, { enumId })
  }

  const updateSortList = params => {
    return $http.post(urls.sort, params)
  }

  return {
    ...crud,
    querySortList,
    updateSortList
  }
}
