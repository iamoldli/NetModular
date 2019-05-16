import { mixins } from 'nm-lib-skins'
import api from '../../../../api/enum'
export default {
  mixins: [mixins.select],
  data() {
    return {
      action: api.select
    }
  }
}
