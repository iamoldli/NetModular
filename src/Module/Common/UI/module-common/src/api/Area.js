import { http } from 'nm-lib-utils'
const root = 'Common/Area/'
const crud = http.crud(root)

const urls = {
  queryChildren: root + 'QueryChildren'
}

/**
 * @description 查询子节点
 */
const queryChildren = parentId => {
  return http.get(urls.queryChildren, { parentId })
}

export default {
  ...crud,
  queryChildren
}
