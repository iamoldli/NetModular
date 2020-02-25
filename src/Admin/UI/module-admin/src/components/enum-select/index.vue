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
    /**模块编码 */
    moduleCode: {
      type: String,
      required: true
    },
    /**枚举名称 */
    enumName: {
      type: String,
      required: true
    },
    /**所在库 */
    libName: String
  },
  methods: {
    query() {
      const { moduleCode, enumName, libName } = this
      return $api.admin.tool.enumSelect({ moduleCode, enumName, libName })
    }
  }
}
</script>
