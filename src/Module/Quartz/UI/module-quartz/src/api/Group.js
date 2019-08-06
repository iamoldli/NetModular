import { http } from 'nm-lib-utils'
const root = 'Quartz/Group/'
const crud = http.crud(root)
const urls = {
  select: root + 'Select'
}

const select = () => {
  return http.get(urls.select)
}

export default {
  ...crud,
  select
}
