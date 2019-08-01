<template>
  <el-button
    class="nm-button"
    :type="options.type"
    :size="getSize"
    :plain="plain"
    :round="round"
    :circle="circle"
    :loading="loading"
    :disabled="disabled"
    :autofocus="autofocus"
    :native-type="nativeType"
    @click="$emit('click')"
    v-nm-has="options.code"
  >
    <nm-icon v-if="!loading&&options.icon" :name="options.icon" />
    <slot>
      <span v-if="!circle&&options.text" class="nm-button-text" v-html="options.text" />
    </slot>
  </el-button>
</template>
<script>
export default {
  name: 'ButtonHas',
  props: {
    // 配置项
    options: {
      type: Object,
      required: true
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
    nativeType: String
  },
  computed: {
    getSize() {
      return this.size || this.fontSize
    }
  }
}
</script>
