import { http } from 'nm-lib-utils'
const root = 'Blog/Article/'
const crud = http.crud(root)

export default {
  ...crud
}
