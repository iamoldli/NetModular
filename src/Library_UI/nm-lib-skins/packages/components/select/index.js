import select from '../../mixins/components/select'
export default {
  name: 'Select',
  mixins: [select],
  props: {
    value: {
      type: [String, Number, Array],
      default: ''
    },
    // 多选
    multiple: Boolean,
    // 可清空
    clearable: {
      type: Boolean,
      default: true
    },
    // 禁用
    disabled: Boolean,
    // 接口方法
    method: Function
  },
  created () {
    this.action = this.method
  }
}
