import { http } from 'nm-lib-utils'
const root = 'PersonnelFiles/UserContact/'
const crud = http.crud(root)

export default {
  ...crud
}
