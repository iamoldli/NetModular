import { http } from 'nm-lib-utils'
const root = 'admin/auditinfo/'
const crud = http.crud(root)
const urls = {
  details: root + 'Details'
}

const details = id => {
  return http.get(urls.details, { id })
}

export default {
  ...crud,
  details
}
