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
    <nm-icon v-if="!noIcon" name="delete"/>
    <span v-if="!circle&&text" class="nm-button-text" v-html="text"/>
  </el-button>
</template>
<script>
export default {
  name: 'ButtonDelete',
  props: {
    type: {
      type: String,
      default: 'text'
    },
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
    // 文本
    text: {
      type: String,
      default: '删除'
    },
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
    getSize () {
      return this.size || this.fontSize
    }
  },
  methods: {
    async remove () {
      this._delete(async () => {
        await this.action(this.id)
        this.$emit('success')
      }, this.msg)
    }
  }
}
</script>
