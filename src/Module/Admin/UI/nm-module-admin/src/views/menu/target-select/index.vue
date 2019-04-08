<script>
import { mixins } from 'nm-lib-skins'
import api from '../../../api/menu'
export default {
  mixins: [mixins.select],
  data () {
    return {
      action: api.getTargetSelect
    }
  }
}
</script>
