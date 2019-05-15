import { mixins } from 'td-lib-skins'
import api from '../../../../api/enum'
export default {
  mixins: [mixins.select],
  data () {
    return {
      action: api.select
    }
  }
}
