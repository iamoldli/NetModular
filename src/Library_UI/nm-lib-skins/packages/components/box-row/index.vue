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
  },
  mounted () {
    this.$nextTick(() => {
      let cols = this.$refs.row.querySelectorAll('.nm-box-col')
      for (let i = 0; i < cols.length; i++) {
        cols[i].style.paddingLeft = this.gutter_
        cols[i].style.paddingRight = this.gutter_
      }
    })
  }
}
</script>
