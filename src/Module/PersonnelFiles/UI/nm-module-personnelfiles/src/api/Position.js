import { http } from 'nm-lib-utils'
const root = 'PersonnelFiles/Position/'
const crud = http.crud(root)

export default {
  ...crud
}
