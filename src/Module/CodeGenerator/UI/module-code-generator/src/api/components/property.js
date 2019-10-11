import module from '../../module'

export default name => {
  const root = `${module.code}/${name}/`
  const crud = $http.crud(root)
  const urls = {
    propertyTypeSelect: root + 'PropertyTypeSelect',
    sort: root + 'sort',
    updateNullable: root + 'UpdateNullable',
    updateShowInList: root + 'UpdateShowInList',
    select: root + 'select'
  }

  /**
   * @description 获取属性类型下拉列表
   */
  const getPropertyTypeSelect = () => {
    return $http.get(urls.propertyTypeSelect)
  }

  const querySortList = classId => {
    return $http.get(urls.sort, { classId })
  }

  const updateSortList = params => {
    return $http.post(urls.sort, params)
  }

  const updateNullable = params => {
    return $http.post(urls.updateNullable, params)
  }

  const updateShowInList = params => {
    return $http.post(urls.updateShowInList, params)
  }

  const select = classId => {
    return $http.get(urls.select, { classId })
  }

  return {
    ...crud,
    getPropertyTypeSelect,
    querySortList,
    updateSortList,
    updateNullable,
    updateShowInList,
    select
  }
}
