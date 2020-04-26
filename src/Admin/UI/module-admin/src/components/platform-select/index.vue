<script>
import { mixins } from 'netmodular-ui'
export default {
  mixins: [mixins.select],
  data() {
    return {
      action: this.query
    }
  },
  props: {
    /**不包含Web */
    noWeb: Boolean
  },
  methods: {
    query() {
      return new Promise(resolve => {
        $api.admin.tool.platformSelect().then(data => {
          let options = data
          if (this.noWeb) {
            options = data.filter(m => m.value > 0)
          }
          resolve(options)
        })
      })
    }
  }
}
</script>
