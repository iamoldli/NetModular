// dialog显示隐藏混入
export default {
  computed: {
    visible_: {
      get () {
        return this.visible
      },
      set (val) {
        this.$emit('update:visible', val)
      }
    }
  },
  props: {
    visible: {
      type: Boolean,
      default: false
    }
  },
  methods: {
    show () {
      this.visible_ = true
    },
    hide () {
      this.visible_ = false
    }
  }
}
