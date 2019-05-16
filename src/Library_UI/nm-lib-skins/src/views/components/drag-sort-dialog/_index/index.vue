<template>
  <nm-tmpl v-bind="tmpl"></nm-tmpl>
</template>
<script>
import page from './page.js'
import attrs from './data/attrs.js'
export default {
  name: page.name,
  data () {
    return {
      tmpl: {
        title: '拖动排序对话框(nm-drag-sort-dialog)',
        icon: 'list',
        attrs
      }
    }
  },
  created () {
    this.setTabName(this.tmpl.title)
  }
}
</script>
