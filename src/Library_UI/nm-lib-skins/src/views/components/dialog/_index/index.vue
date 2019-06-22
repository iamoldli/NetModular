<template>
  <td-tmpl v-bind="tmpl"></td-tmpl>
</template>
<script>
import page from './page.js'
import attrs from './data/attrs.js'
import events from './data/events.js'
import slots from './data/slots'
export default {
  name: page.name,
  data() {
    return {
      tmpl: {
        title: '对话框(td-dialog)',
        icon: 'dialog',
        attrs,
        events,
        slots
      }
    }
  },
  created() {
    this.setTabName(this.tmpl.title)
  }
}
</script>
