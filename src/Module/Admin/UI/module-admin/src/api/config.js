import { http } from 'nm-lib-utils'
const root = 'admin/config/'
const crud = http.crud(root)
const urls = {
  getValue: root + 'GetValue'
}

const getValue = key => {
  return http.get(urls.getValue, { key })
}

export default {
  ...crud,
  getValue
}
