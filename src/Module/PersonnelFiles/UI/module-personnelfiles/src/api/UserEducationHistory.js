import { http } from 'nm-lib-utils'
const root = 'PersonnelFiles/UserEducationHistory/'
const crud = http.crud(root)

export default {
  ...crud
}
