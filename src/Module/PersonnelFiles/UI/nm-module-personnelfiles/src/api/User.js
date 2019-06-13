import { http } from 'nm-lib-utils'
const root = 'PersonnelFiles/User/'
const crud = http.crud(root)

export default {
  ...crud
}
