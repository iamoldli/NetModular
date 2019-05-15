<template>
  <nm-tmpl v-bind="tmpl"></nm-tmpl>
</template>
<script>
import page from './page.js'
import attrs from './data/attrs.js'
export default {
  name: page.name,
  data() {
    return {
      tmpl: {
        title: '拖动排序(nm-drag-sort)',
        icon: 'list',
        attrs
      }
    }
  },
  created() {
    this.setTabName(this.tmpl.title)
  }
}
</script>
