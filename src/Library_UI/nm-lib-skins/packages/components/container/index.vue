<template>
  <section class="nm-container" :class="{'is-horizontal':horizontal}">
    <section class="nm-container-main">
      <nm-scrollbar v-if="scrollbar" :horizontal="horizontal">
        <slot />
      </nm-scrollbar>
      <slot v-else />
    </section>
  </section>
</template>
<script>
export default {
  name: 'Container',
  props: {
    /** 滚动条 */
    scrollbar: Boolean,
    /** 是否显示水平滚动条 */
    horizontal: Boolean
  }
}
</script>
