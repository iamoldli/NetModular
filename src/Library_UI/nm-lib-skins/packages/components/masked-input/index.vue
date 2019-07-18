<template>
  <div class="el-input el-input--small">
    <masked-input
      type="text"
      class="el-input__inner"
      v-model="value_"
      :mask="mask"
      :guide="guide"
      :placeholderChar="placeholderChar"
      :keepCharPositions="keepCharPositions"
      :showMask="showMask"
    ></masked-input>
  </div>
</template>
<script>
// https://github.com/text-mask/text-mask/blob/master/componentDocumentation.md#readme
import MaskedInput from 'vue-text-mask'

export default {
  name: 'MaskInput',
  components: { MaskedInput },
  data() {
    return {
      value_: this.value
    }
  },
  props: {
    value: {
      type: [Number, String]
    },
    mask: {
      type: [Array, Function],
      required: true
    },
    /** 是否制导模式 */
    guide: {
      type: Boolean,
      default: true
    },
    placeholderChar: String,
    keepCharPositions: Boolean,
    showMask: Boolean
  },
  watch: {
    value_(val) {
      this.$emit('input', val)
    }
  }
}
</script>
