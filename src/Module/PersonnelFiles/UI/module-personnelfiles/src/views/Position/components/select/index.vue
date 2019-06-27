<script>
import { mixins } from 'nm-lib-skins'
import api from '../../../../api/Position'
export default {
  mixins: [mixins.select],
  data() {
    return {
      action: this.query
    }
  },
  props: {
    departmentId: {
      type: String,
      required: true
    }
  },
  methods: {
    query() {
      if (this.departmentId) {
        return api.select(this.departmentId)
      } else {
        return new Promise(resolve => {
          resolve([])
        })
      }
    }
  },
  watch: {
    departmentId() {
      this.refresh()
    }
  }
}
</script>
