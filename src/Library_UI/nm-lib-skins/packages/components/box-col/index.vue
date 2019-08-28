<template>
  <section :class="['nm-box-col',span ? 'nm-box-col-' + span : '']" :style="style">
    <slot />
  </section>
</template>
<script>
export default {
  name: 'BoxCol',
  props: {
    span: Number
  },
  computed: {
    style() {
      return {
        paddingLeft: this.$parent.gutter_,
        paddingRight: this.$parent.gutter_
      }
    }
  }
}
</script>
