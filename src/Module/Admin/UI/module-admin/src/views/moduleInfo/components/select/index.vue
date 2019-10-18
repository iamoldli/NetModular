<script>
import { mixins } from 'nm-lib-skins'
export default {
  mixins: [mixins.select],
  data() {
    return {
      action: $api.admin.moduleInfo.select
    }
  }
}
</script>
