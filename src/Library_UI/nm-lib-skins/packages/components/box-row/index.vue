<template>
  <section ref="row" class="nm-box-row" :style="{height}">
    <slot/>
  </section>
</template>
<script>
export default {
  name: 'BoxRow',
  props: {
    /** 间隔 */
    gutter: {
      type: Number,
      default: 5
    },
    /** 高度 */
    height: {
      type: String,
      required: true
    }
  },
  computed: {
    gutter_ () {
      return this.gutter / 2 + 'px'
    }
  }
}
</script>
