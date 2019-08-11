<script>
import { mixins } from 'nm-lib-skins'
import api from '../../../../api/Group'
export default {
  mixins: [mixins.select],
  data() {
    return {
      action: api.select
    }
  }
}
</script>
