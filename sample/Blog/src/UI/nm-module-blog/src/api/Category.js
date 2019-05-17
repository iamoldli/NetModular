import { http } from 'nm-lib-utils'
const root = 'Blog/Category/'
const crud = http.crud(root)

export default {
  ...crud
}
