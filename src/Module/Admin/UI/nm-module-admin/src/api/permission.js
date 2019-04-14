import { http } from 'nm-lib-utils'
const root = 'admin/permission/'
const crud = http.crud(root)

const urls = {
  controllers: root + 'Controllers',
  actions: root + 'Actions',
  sync: root + 'sync'
}

/**
 * @description 同步
 */
const sync = () => {
  return http.post(urls.sync)
}

export default {
  ...crud,
  sync
}
