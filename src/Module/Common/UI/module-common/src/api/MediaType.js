import { http } from 'nm-lib-utils'
const root = 'Common/MediaType/'
const crud = http.crud(root)

export default {
  ...crud
}
