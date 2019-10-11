<script>
import { mixins } from 'nm-lib-skins'

const api = $api.quartz.job

export default {
  mixins: [mixins.select],
  data() {
    return {
      action: api.moduleSelect
    }
  }
}
</script>
