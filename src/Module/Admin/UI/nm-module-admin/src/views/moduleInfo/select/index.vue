<script>
import { mixins } from 'nm-lib-skins'
import api from '../../../api/moduleInfo'
export default {
  mixins: [mixins.select],
  data () {
    return {
      action: api.select
    }
  }
}
</script>
