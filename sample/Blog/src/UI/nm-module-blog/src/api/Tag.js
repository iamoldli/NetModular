import { http } from 'nm-lib-utils'
const root = 'Blog/Tag/'
const crud = http.crud(root)

export default {
  ...crud
}
