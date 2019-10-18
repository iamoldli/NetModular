import module from '../../module'

export default name => {
  const root = `${module.code}/${name}/`
  const crud = $http.crud(root)
  const urls = {
    details: root + 'Details',
    queryLatestWeekPv: root + 'QueryLatestWeekPv'
  }

  const details = id => {
    return $http.get(urls.details, { id })
  }

  const queryLatestWeekPv = () => {
    return $http.get(urls.queryLatestWeekPv)
  }

  return {
    ...crud,
    details,
    queryLatestWeekPv
  }
}
