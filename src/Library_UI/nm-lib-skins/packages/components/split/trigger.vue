<template>
  <div :class="classes">
    <div class="nm-split-trigger-bar-con">
      <i class="nm-split-trigger-bar" v-once v-for="i in 8" :key="`trigger-${i}`"></i>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    mode: String
  },
  data() {
    return {
      initOffset: 0
    }
  },
  computed: {
    isVertical() {
      return this.mode === 'vertical'
    },
    classes() {
      return [
        'nm-split-trigger',
        this.isVertical ? `nm-split-trigger-vertical` : `nm-split-trigger-horizontal`
      ]
    }
  }
}
</script>
