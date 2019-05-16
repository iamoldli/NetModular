<template>
  <nm-tmpl v-bind="tmpl"></nm-tmpl>
</template>
<script>
import page from './page.js'
import attrs from './data/attrs.js'
import events from './data/events.js'
export default {
  name: page.name,
  data () {
    return {
      tmpl: {
        title: '颜色选择器(nm-color-picker)',
        icon: 'list',
        attrs,
        events
      }
    }
  },
  created () {
    this.setTabName(this.tmpl.title)
  }
}
</script>
