import { http } from 'nm-lib-utils'
const root = 'codegenerator/class/'
const crud = http.crud(root)
const urls = {
  baseEntityTypeSelect: root + 'BaseEntityTypeSelect',
  buildCode: root + 'BuildCode'
}

/**
 * @description 获取基类类型下拉列表
 */
const getBaseEntityTypeSelect = () => {
  return http.get(urls.baseEntityTypeSelect)
}
/** 生成代码 */
const buildCode = id => {
  return http.get(urls.buildCode, { id }, { responseType: 'blob' })
}
export default {
  ...crud,
  getBaseEntityTypeSelect,
  buildCode
}
