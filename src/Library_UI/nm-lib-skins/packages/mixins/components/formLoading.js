// 表单页的加载效果
export default {
  data () {
    return {
      loading: false
    }
  },
  watch: {
    loading () {
      this.$nextTick(() => {
        if (this.loading) {
          this.$refs.form.openLoading()
        } else {
          this.$refs.form.closeLoading()
        }
      })
    }
  }
}
