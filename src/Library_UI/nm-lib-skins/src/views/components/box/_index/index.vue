<template>
  <nm-tmpl v-bind="tmpl">
    <el-alert title="滚动条" type="warning" description="如果设置了no-scrollbar属性为true，则始终不显示滚动条，反之，如果设置了height或者page属性，则显示滚动条" show-icon :closable="false"></el-alert>
  </nm-tmpl>
</template>
<script>
import page from './page.js'
import attrs from './data/attrs.js'
import slots from './data/slots.js'
import methods from './data/methods.js'
import events from './data/events.js'
export default {
  name: page.name,
  data() {
    return {
      val: '',
      tmpl: {
        title: '盒子(nm-box)',
        icon: 'card',
        attrs,
        slots,
        methods,
        events
      }
    }
  },
  created() {
    this.setTabName(this.tmpl.title)
  }
}
</script>
