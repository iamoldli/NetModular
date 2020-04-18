const { edit, update } = $api.admin.config
export default {
  data() {
    return {
      type: 1,
      form: {
        header: false,
        action: this.update,
        model: {}
      }
    }
  },
  methods: {
    update() {
      return update({ type: this.type, code: this.code, json: JSON.stringify(this.form.model) })
    }
  },
  created() {
    edit({ type: this.type, code: this.code }).then(data => {
      this.form.model = data
    })
  }
}
