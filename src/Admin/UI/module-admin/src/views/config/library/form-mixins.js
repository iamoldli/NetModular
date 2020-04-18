import { mapMutations } from 'vuex'
const { edit, update } = $api.admin.config
export default {
  data() {
    return {
      type: 0,
      code: '',
      form: {
        header: false,
        action: this.update
      }
    }
  },
  methods: {
    ...mapMutations('app/config', { setUIConfig: 'init' }),
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
