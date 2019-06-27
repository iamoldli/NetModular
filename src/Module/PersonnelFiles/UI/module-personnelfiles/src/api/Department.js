import { http } from 'nm-lib-utils'
const root = 'PersonnelFiles/Department/'
const crud = http.crud(root)
const urls = {
  tree: root + 'Tree'
}

/**
 * 获取部门树
 * @param {公司编号} companyId
 */
const getTree = companyId => {
  return http.get(urls.tree, { companyId })
}
export default {
  ...crud,
  getTree
}
