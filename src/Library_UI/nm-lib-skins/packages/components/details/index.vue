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
  data() {
    return {
      /** 表单模型 */
      model: {},
      loading_: false
    }
  },
  props: {
    // 查询方法
    action: {
      type: Function,
      required: true
    },
    /** 标签的宽度 */
    labelWidth: {
      type: String,
      default: '100px'
    },
    // 不显示loading
    noLoading: Boolean
  },
  computed: {
    ...mapState('app/loading', { loadingText: 'text', loadingBackground: 'background', loadingSpinner: 'spinner' }),
    loading() {
      return !this.noLoading && this.loading_
    }
  },
  methods: {
    query() {
      this.loading_ = true
      this.action().then(model => {
        this.model = model
        this.loading_ = false
      }).catch(() => {
        this.loading_ = false
      })
    }
  },
  mounted() {
    this.$nextTick(() => {
      const labels = this.$refs.details.querySelectorAll('.nm-details-label')
      labels.forEach(element => {
        element.style.width = this.labelWidth
      })
    })
  }
}
</script>
