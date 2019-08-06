<script>
import { mixins } from 'nm-lib-skins'
import api from '../../../../api/Job'
export default {
  mixins: [mixins.select],
  data() {
    return {
      action: api.moduleSelect
    }
  }
}
</script>
