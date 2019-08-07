import { http } from 'nm-lib-utils'
const root = 'Common/Area/'
const crud = http.crud(root)

const urls = {
  queryChildren: root + 'QueryChildren'
}

/**
 * @description 查询子节点
 */
const queryChildren = parentCode => {
  return http.get(urls.queryChildren, { parentCode })
}

export default {
  ...crud,
  queryChildren
}
