import module from '../../module'

export default name => {
  const root = `${module.code}/${name}/`
  const crud = $http.crud(root)
  const urls = {
    details: root + 'Details',
    queryLatestWeekPv: root + 'QueryLatestWeekPv',
    export: root + 'Export'
  }

  const details = id => {
    return $http.get(urls.details, { id })
  }

  const queryLatestWeekPv = () => {
    return $http.get(urls.queryLatestWeekPv)
  }

  const exportData = params => {
    return $http.export(urls.export, params)
  }

  return {
    ...crud,
    details,
    queryLatestWeekPv,
    exportData
  }
}
