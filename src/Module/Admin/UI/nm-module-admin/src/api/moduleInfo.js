import { http } from 'nm-lib-utils'
const root = 'admin/moduleinfo/'
const crud = http.crud(root)
const urls = {
  select: root + 'select'
}

/**
 * @description 下拉框
 */
const select = () => {
  return http.get(urls.select)
}

export default { ...crud, select }
