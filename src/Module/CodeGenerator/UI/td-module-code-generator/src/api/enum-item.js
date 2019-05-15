import { http } from 'td-lib-utils'
const root = 'codegenerator/enumitem/'
const crud = http.crud(root)
const urls = {
  sort: root + 'sort'
}
const querySortList = enumId => {
  return http.get(urls.sort, { enumId })
}

const updateSortList = params => {
  return http.post(urls.sort, params)
}

export default {
  ...crud,
  querySortList,
  updateSortList
}
