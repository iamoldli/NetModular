<template>
  <nm-tmpl v-bind="tmpl"></nm-tmpl>
</template>
<script>
import page from './page.js'
import attrs from './data/attrs.js'
import methods from './data/methods'
import events from './data/events.js'
export default {
  name: page.name,
  data () {
    return {
      tmpl: {
        title: '基础表单(nm-form)',
        icon: 'list',
        attrs,
        methods,
        events
      }
    }
  },
  created () {
    this.setTabName(this.tmpl.title)
  }
}
</script>
