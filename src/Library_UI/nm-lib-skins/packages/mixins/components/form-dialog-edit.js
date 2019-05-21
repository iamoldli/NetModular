import dialog from './dialog'
export default {
  mixins: [dialog],
  data () {
    return {
      // 存储表单数据
      model_: {},
      form: {
        icon: 'edit',
        model: {
          id: ''
        },
        customResetFunction: this.onReset,
        loading: false
      },
      on: {
        success: this.onSuccess,
        open: this.onOpen
      }
    }
  },
  props: {
    id: {
      type: [String, Number],
      required: true
    }
  },
  methods: {
    async get () {
      this.form.loading = true
      this.form.model = await this.api.edit(this.id)
      this.model_ = this.$_.merge({}, this.form.model)
      this.form.loading = false
    },
    onReset () {
      this.form.model = this.$_.merge({}, this.model_)
    },
    onSuccess () {
      this.$emit('success')
    },
    onOpen () {
      if (!this.id) {
        this._warning('请选择要编辑的数据~')
        this.$refs.form.reset()
        return
      }

      if (this.id !== this.form.model.id) {
        this.get()
      }
    }
  }
}
