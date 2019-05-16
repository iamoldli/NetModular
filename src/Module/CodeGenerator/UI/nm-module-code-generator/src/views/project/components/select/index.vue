<script>
import { mixins } from 'nm-lib-skins'
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
