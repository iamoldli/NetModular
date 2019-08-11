import { http } from 'nm-lib-utils'
const root = 'PersonnelFiles/Position/'
const crud = http.crud(root)
const urls = {
  select: root + 'Select'
}

const select = departmentId => {
  return http.get(urls.select, { departmentId })
}

export default {
  ...crud,
  select
}
