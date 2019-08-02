import { http } from 'nm-lib-utils'
const root = 'Quartz/JobClass/'
const crud = http.crud(root)

export default {
  ...crud
}
