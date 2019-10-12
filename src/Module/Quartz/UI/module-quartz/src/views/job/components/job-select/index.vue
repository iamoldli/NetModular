<script>
import { mixins } from 'nm-lib-skins'

const api = $api.quartz.job

export default {
  mixins: [mixins.select],
  data() {
    return {
      action: this.query
    }
  },
  props: {
    moduleId: {
      type: String
    }
  },
  methods: {
    query() {
      if (this.moduleId) {
        return api.jobSelect(this.moduleId)
      } else {
        return new Promise(resolve => {
          resolve([])
        })
      }
    }
  },
  watch: {
    moduleId() {
      this.refresh()
    }
  }
}
</script>
