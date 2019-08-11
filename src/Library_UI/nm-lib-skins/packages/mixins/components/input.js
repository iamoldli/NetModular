export default {
  data() {
    return {
      value_: this.value
    }
  },
  props: {
    value: {
      type: String
    }
  },
  methods: {
    onChange(val) {
      this.value_ = val
      this.$emit('input', this.value_)
      this.$emit('change', this.value_)
    }
  },
  watch: {
    value(val) {
      this.value_ = val
    }
  }
}
