<template>
  <section
    ref="details"
    class="nm-details"
    v-loading="loading"
    :element-loading-text="loadingText"
    :element-loading-background="loadingBackground"
    :element-loading-spinner="loadingSpinner"
  >
    <slot :model="model"/>
  </section>
</template>
<script>
import { mapState } from 'vuex'
export default {
  name: 'Details',
  data () {
    return {
      /** 表单模型 */
      loading: {},
      model: {}
    }
  },
  props: {
    /** 标签的宽度 */
    labelWidth: {
      type: String,
      default: '100px'
    },
    // 查询方法
    action: {
      type: Function,
      required: true
    }
  },
  computed: {
    ...mapState('app/loading', { loadingText: 'text', loadingBackground: 'background', loadingSpinner: 'spinner' })
  },
  methods: {
    query () {
      this.loading = true
      this.action().then(model => {
        this.model = model
        this.loading = false
      }).catch(() => {
        this.loading = false
      })
    }
  },
  mounted () {
    this.$nextTick(() => {
      const labels = this.$refs.details.querySelectorAll('.nm-details-label')
      labels.forEach(element => {
        element.style.width = this.labelWidth
      })
    })
  }
}
</script>
