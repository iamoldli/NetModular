<template>
  <nm-mask-input :class="{'nm-input-number':true,'align-right':alignRight}" :value="value_" :mask="mask" :placeholder="placeholder" @change="onChange">
    <template slot="prepend">
      <slot v-if="$slots.prepend" name="prepend"></slot>
    </template>
    <template slot="prefix">
      <slot v-if="$slots.prefix" name="prefix"></slot>
    </template>
    <template slot="suffix">
      <slot v-if="$slots.suffix" name="suffix"></slot>
    </template>
    <template slot="append">
      <slot v-if="$slots.append" name="append"></slot>
    </template>
  </nm-mask-input>
</template>
<script>
import mixins from '../../mixins/components/input'
import createNumberMask from '../mask-input/masks/createNumberMask'
export default {
  name: 'InputNumber',
  mixins: [mixins],
  props: {
    /** 前缀 */
    prefix: {
      type: String,
      default: ''
    },
    /** 后缀 */
    suffix: {
      type: String,
      default: ''
    },
    /** 是否包含千分位分隔符 */
    includeThousandsSeparator: {
      type: Boolean,
      default: false
    },
    /** 千分位分隔符,默认',' */
    thousandsSeparatorSymbol: {
      type: String,
      default: ','
    },
    /** 允许小数位 */
    allowDecimal: {
      type: Boolean,
      default: false
    },
    /** 小数位符号，默认'.' */
    decimalSymbol: {
      type: String,
      default: '.'
    },
    /** 小数位长度，默认'2' */
    decimalLimit: {
      type: Number,
      default: 2
    },
    /** 整数位长度，默认为null，没有限制 */
    integerLimit: Number,
    /** 是否始终显示小数位 */
    requireDecimal: {
      type: Boolean,
      default: false
    },
    /** 是否允许负数 */
    allowNegative: {
      type: Boolean,
      default: false
    },
    placeholder: {
      type: String,
      default: ''
    },
    /** 是否右对齐 */
    alignRight: {
      type: Boolean,
      default: false
    }
  },
  computed: {
    mask() {
      return createNumberMask({
        prefix: this.prefix,
        suffix: this.suffix,
        includeThousandsSeparator: this.includeThousandsSeparator,
        thousandsSeparatorSymbol: this.thousandsSeparatorSymbol,
        allowDecimal: this.allowDecimal,
        decimalSymbol: this.decimalSymbol,
        decimalLimit: this.decimalLimit,
        integerLimit: this.integerLimit,
        requireDecimal: this.requireDecimal,
        allowNegative: this.allowNegative
      })
    }
  }
}

</script>
<style lang="scss">
.nm-input-number {
  &.align-right {
    .el-input__inner {
      text-align: right;
    }
  }
}
</style>
