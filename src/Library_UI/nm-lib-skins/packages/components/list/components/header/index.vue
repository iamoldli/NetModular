<template>
  <header class="nm-list-header">
    <div class="nm-list-icon">
      <nm-icon :name="icon||'list'" />
    </div>
    <div class="nm-list-title">{{title}}</div>
    <div class="nm-list-header-toolbar">
      <!--工具栏插槽-->
      <slot name="toolbar" />
      <!--刷新按钮-->
      <nm-button v-if="!noRefresh" icon="refresh" @click="refresh" />
      <!--全屏按钮-->
      <nm-button v-if="!noFullscreen" :icon="fullscreen ? 'min' : 'max'" @click="onFullscreenClict" />
    </div>
  </header>
</template>
<script>

export default {
  props: {
    value: Object,
    /** 标题 */
    title: String,
    /** 图标 */
    icon: String,
    /** 不显示全屏按钮 */
    noFullscreen: Boolean,
    /** 是否全屏 */
    fullscreen: Boolean,
    /** 不显示刷新按钮 */
    noRefresh: Boolean
  },
  methods: {
    onFullscreenClict() {
      this.$emit('update:fullscreen', !this.fullscreen)
    },
    query() {
      this.$parent.page.index = 1
      this.$parent.query()
    },
    reset() {
      this.$parent.reset()
    },
    refresh() {
      this.$parent.refresh()
    }
  }
}
</script>
