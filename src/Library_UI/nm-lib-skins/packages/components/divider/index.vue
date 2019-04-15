<template>
  <div ref="Divider" :class="classes" :style="{color:borderColor}">
    <span v-if="hasSlot" class="nm-divider-inner-text" :style="{color:textColor}">
      <slot></slot>
    </span>
  </div>
</template>

<script>
import { oneOf } from 'nm-lib-utils/src/utils/assist'

export default {
  name: 'Divider',
  props: {
    /** 水平还是垂直类型，可选值为 horizontal 或 vertical */
    type: {
      type: String,
      default: 'horizontal',
      validator (value) {
        return oneOf(value, ['horizontal', 'vertical'])
      }
    },
    /** 分割线标题的位置，可选值为 left、right 或 center */
    orientation: {
      type: String,
      default: 'center',
      validator (value) {
        return oneOf(value, ['left', 'right', 'center'])
      }
    },
    /** 是否虚线 */
    dashed: {
      type: Boolean,
      default: false
    },
    /** 尺寸，可选值为 small 或 default */
    size: {
      validator (value) {
        return oneOf(value, ['small', 'default'])
      },
      default: 'default'
    },
    /** 边框颜色 */
    borderColor: {
      type: String,
      default: '#e8eaec'
    },
    /** 文本的颜色 */
    textColor: {
      type: String,
      default: '#909399'
    }
  },
  computed: {
    hasSlot () {
      return !!this.$slots.default
    },
    classes () {
      return [
        'nm-divider',
        `nm-divider-${this.type}`,
        `nm-divider-${this.size}`,
        {
          [`nm-divider-with-text`]: this.hasSlot && this.orientation === 'center',
          [`nm-divider-with-text-${this.orientation}`]: this.hasSlot,
          [`nm-divider-dashed`]: !!this.dashed
        }
      ]
    }
  }
}
</script>
