<script>
import { mixins } from 'td-lib-skins'
import api from '../../../api/project'
export default {
  mixins: [mixins.select],
  data () {
    return {
      action: api.select
    }
  }
}
</script>
