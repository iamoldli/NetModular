import { http } from 'nm-lib-utils'
const root = 'PersonnelFiles/UserWorkHistory/'
const crud = http.crud(root)

export default {
  ...crud
}
