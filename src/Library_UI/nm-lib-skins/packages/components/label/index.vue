<template>
  <span :class="class_">
    <slot>{{text}}</slot>
  </span>
</template>
<script>
export default {
  name: 'Label',
  props: {
    /** 显示文本 */
    text: {
      type: String,
      default: ''
    },
    type: {
      type: String,
      default: ''
    },
    /** 左边箭头 */
    arrowed: {
      type: Boolean,
      default: false
    },
    /** 左侧内箭头 */
    arrowedIn: {
      type: Boolean,
      default: false
    },
    /** 右侧箭头 */
    arrowedRight: {
      type: Boolean,
      default: false
    },
    /** 右侧内箭头 */
    arrowedRightIn: {
      type: Boolean,
      default: false
    }
  },
  computed: {
    class_() {
      return [
        'nm-label',
        this.type ? `nm-label-${this.type}` : '',
        this.arrowed ? 'arrowed' : '',
        this.arrowedIn ? 'arrowed-in' : '',
        this.arrowedRight ? 'arrowed-right' : '',
        this.arrowedRightIn ? 'arrowed-right-in' : ''
      ]
    }
  }
}
</script>
