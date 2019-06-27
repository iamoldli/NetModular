<script>
import { mixins } from 'nm-lib-skins'
import api from '../../../../api/Company'
export default {
  mixins: [mixins.select],
  data() {
    return {
      action: api.select
    }
  }
}
</script>
