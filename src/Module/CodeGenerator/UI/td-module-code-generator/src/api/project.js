import { http } from 'td-lib-utils'
const root = 'codegenerator/project/'
const crud = http.crud(root)
const urls = {
  select: root + 'select',
  buildCode: root + 'BuildCode'
}

/**
 * @description 查询字典下拉列表
 * @param {Object} params 参数
 */
const select = params => {
  return http.get(urls.select, params)
}
/** 生成代码 */
const buildCode = params => {
  return http.post(urls.buildCode, params, { responseType: 'blob' })
}

export default {
  ...crud,
  select,
  buildCode
}
