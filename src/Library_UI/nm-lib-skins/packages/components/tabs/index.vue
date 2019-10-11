<template>
  <section :class="['nm-tabs',this.fullscreen_?'fullscreen':'']">
    <section class="nm-tabs-toolbar">
      <!--全屏按钮-->
      <nm-button v-if="fullscreen" :icon="this.fullscreen_ ? 'min' : 'max'" @click="onFullscreen" />
    </section>
    <slot />
  </section>
</template>
<script>
export default {
  name: 'Tabs',
  data() {
    return {
      fullscreen_: false
    }
  },
  props: {
    fullscreen: {
      type: Boolean,
      default: true
    }
  },
  methods: {
    /** 全屏事件 */
    onFullscreen() {
      if (this.fullscreen) {
        this.fullscreen_ = !this.fullscreen_

        // 全屏事件
        this.$emit('fullscreen-change', this.fullscreen_)
      }
    }
  }
}
</script>
