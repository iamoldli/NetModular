<template>
  <nm-tmpl v-bind="tmpl"></nm-tmpl>
</template>
<script>
import page from './page.js'
import attrs from './data/attrs.js'
import slots from './data/slots.js'
import events from './data/events.js'
import methods from './data/methods.js'
export default {
  name: page.name,
  data () {
    return {
      tmpl: {
        title: '多选框(nm-checkbox-group)',
        icon: 'checkbox',
        attrs,
        slots,
        events,
        methods
      }
    }
  },
  created () {
    this.setTabName(this.tmpl.title)
  }
}
</script>
