import { http } from 'td-lib-utils'
const root = 'codegenerator/ModelProperty/'
const crud = http.crud(root)
const urls = {
  sort: root + 'sort',
  updateNullable: root + 'UpdateNullable',
  select: root + 'select',
  importFromEntity: root + 'importFromEntity'
}

const querySortList = params => {
  return http.get(urls.sort, params)
}

const updateSortList = params => {
  return http.post(urls.sort, params)
}

const updateNullable = params => {
  return http.post(urls.updateNullable, params)
}

const select = params => {
  return http.get(urls.select, params)
}

const importFromEntity = params => {
  return http.post(urls.importFromEntity, params)
}

export default {
  ...crud,
  querySortList,
  updateSortList,
  updateNullable,
  select,
  importFromEntity
}
