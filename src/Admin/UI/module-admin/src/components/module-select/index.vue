<script>
import { mixins } from 'netmodular-ui'
export default {
  mixins: [mixins.select],
  data() {
    return {
      action: $api.admin.module.select
    }
  }
}
</script>
