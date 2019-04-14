import { http } from 'nm-lib-utils'
const root = 'admin/moduleinfo/'
const crud = http.crud(root)
const urls = {
  select: root + 'select',
  sync: root + 'sync'
}

/**
 * @description 下拉框
 */
const select = () => {
  return http.get(urls.select)
}

/**
 * @description 同步
 */
const sync = () => {
  return http.post(urls.sync)
}

export default { ...crud, select, sync }
