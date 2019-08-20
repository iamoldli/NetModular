<template>
  <el-button
    class="nm-button"
    :type="type"
    :size="getSize"
    :plain="plain"
    :round="round"
    :circle="circle"
    :loading="loading"
    :disabled="disabled"
    :autofocus="autofocus"
    :native-type="nativeType"
    @click="remove"
  >
    <nm-icon v-if="!noIcon" :name="icon" />
    <span v-if="!circle&&text" class="nm-button-text" v-html="text" />
  </el-button>
</template>
<script>
export default {
  name: 'ButtonDelete',
  props: {
    options: Object,
    /** 尺寸，默认或者为空时，按照皮肤的字号设置 */
    size: String,
    /** 是否朴素按钮 */
    plain: Boolean,
    /** 是否圆角按钮 */
    round: Boolean,
    /** 是否圆形按钮 */
    circle: Boolean,
    /** 是否加载中状态 */
    loading: Boolean,
    /** 是否禁用状态 */
    disabled: Boolean,
    /** 是否默认聚焦 */
    autofocus: Boolean,
    /** 原生 type 属性 button/submit/reset */
    nativeType: String,
    // 不显示图标
    noIcon: Boolean,
    // 删除方法
    action: {
      type: Function,
      required: true
    },
    // id
    id: [Number, String, Object, Array],
    // 提示语
    msg: String
  },
  computed: {
    getSize() {
      return this.size || this.fontSize
    },
    type() {
      if (this.options && this.options.type) {
        return this.options.type
      }
      return 'text'
    },
    icon() {
      if (this.options && this.options.icon) {
        return this.options.icon
      }
      return 'delete'
    },
    text() {
      if (this.options && this.options.text) {
        return this.options.text
      }
      return '删除'
    }
  },
  methods: {
    async remove() {
      this._delete(async () => {
        await this.action(this.id)
        this.$emit('success')
      }, this.msg).catch(() => {
        console.log('取消')
      })
    }
  }
}
</script>
