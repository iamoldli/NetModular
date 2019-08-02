import { http } from 'nm-lib-utils'
const root = 'Quartz/Job/'
const crud = http.crud(root)

export default {
  ...crud
}
