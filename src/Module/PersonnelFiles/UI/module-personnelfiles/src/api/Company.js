import { http } from 'nm-lib-utils'
const root = 'PersonnelFiles/Company/'
const crud = http.crud(root)
const urls = {
  select: root + 'Select'
}

/**
 * 下拉列表
 */
const select = () => {
  return http.get(urls.select)
}

export default {
  ...crud,
  select
}
