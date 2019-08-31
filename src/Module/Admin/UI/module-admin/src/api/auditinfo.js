import { http } from 'nm-lib-utils'
const root = 'admin/auditinfo/'
const crud = http.crud(root)
const urls = {
  details: root + 'Details',
  queryLatestWeekPv: root + 'QueryLatestWeekPv'
}

const details = id => {
  return http.get(urls.details, { id })
}

const queryLatestWeekPv = () => {
  return http.get(urls.queryLatestWeekPv)
}

export default {
  ...crud,
  details,
  queryLatestWeekPv
}
