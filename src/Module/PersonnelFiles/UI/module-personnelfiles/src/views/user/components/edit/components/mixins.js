export default {
  data() {
    return {
      // 是否已查询了数据，方式切换tab时重复查询
      hasGet: false,
      model_: {},
      form: {
        noLoading: true
      },
      on: {
        success: this.onSuccess,
        error: this.onError,
        'validate-error': this.onValidateError
      }
    }
  },
  methods: {
    async edit(id) {
      if (!this.hasGet) {
        this.form.model = await this.editAction(id)
        this.model_ = this.$_.merge({}, this.form.model)
        this.hasGet = true

        // 回调方法
        if (this.callback) {
          this.callback()
        }
      }
    },
    refresh() {
      this.hasGet = false
    },
    reset() {
      this.form.model = this.$_.merge({}, this.model_)
      this.hasGet = false
    },
    submit() {
      this.$refs.form.submit()
    },
    onSuccess() {
      this.$emit('success')
    },
    onError() {
      this.$emit('error')
    },
    onValidateError() {
      this.$emit('validate-error')
    }
  }
}
