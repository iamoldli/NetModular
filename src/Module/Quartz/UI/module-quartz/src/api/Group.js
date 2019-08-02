import { http } from 'nm-lib-utils'
const root = 'Quartz/Group/'
const crud = http.crud(root)

export default {
  ...crud
}
