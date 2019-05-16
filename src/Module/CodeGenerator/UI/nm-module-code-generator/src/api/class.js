import { http } from 'nm-lib-utils'
const root = 'codegenerator/class/'
const crud = http.crud(root)
const urls = {
  baseEntityTypeSelect: root + 'BaseEntityTypeSelect'
}

/**
 * @description 获取基类类型下拉列表
 */
const getBaseEntityTypeSelect = () => {
  return http.get(urls.baseEntityTypeSelect)
}

export default {
  ...crud,
  getBaseEntityTypeSelect
}
