<template>
  <div
    :class="['nm-mask-input','el-input',
    fontSize ? 'el-input--' + fontSize : '',
    {
      'is-disabled': disabled,
      'el-input-group': $slots.prepend || $slots.append,
      'el-input-group--append': $slots.append,
      'el-input-group--prepend': $slots.prepend,
      'el-input--prefix': $slots.prefix,
      'el-input--suffix': $slots.suffix
    }
    ]"
  >
    <div v-if="$slots.prepend" class="el-input-group__prepend">
      <slot name="prepend"></slot>
    </div>
    <span v-if="$slots.prefix" class="el-input__prefix">
      <span class="el-input__prefix-inner">
        <slot name="prefix" />
      </span>
    </span>
    <masked-input
      type="text"
      class="el-input__inner"
      :value="value_"
      :mask="mask"
      :guide="guide"
      :placeholderChar="placeholderChar"
      :keepCharPositions="keepCharPositions"
      :showMask="showMask"
      :placeholder="placeholder"
      @input="onChange"
    ></masked-input>
    <span v-if="$slots.suffix" class="el-input__suffix">
      <span class="el-input__suffix-inner">
        <slot name="suffix" />
      </span>
    </span>
    <div v-if="$slots.append" class="el-input-group__append">
      <slot name="append"></slot>
    </div>
  </div>
</template>
<script>
// https://github.com/text-mask/text-mask/blob/master/componentDocumentation.md#readme
import MaskedInput from 'vue-text-mask'
import mixins from '../../mixins/components/input'
export default {
  name: 'MaskInput',
  mixins: [mixins],
  components: { MaskedInput },
  props: {
    mask: {
      type: [Array, Function],
      required: true
    },
    /** 是否指导模式 */
    guide: Boolean,
    /** 提示文本 */
    placeholder: String,
    /** 未输入位置的占位符 */
    placeholderChar: String,
    /** 是否一直开始占位符 */
    keepCharPositions: Boolean,
    showMask: Boolean,
    disabled: Boolean,
    customClass: Array
  }
}
</script>
