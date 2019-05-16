<template>
  <nm-tmpl v-bind="tmpl">
    <el-alert title="盒子栅格化" type="warning" description="主要用于盒子的快速布局，通过指定 height 属性，可以让每一行中的盒子都一样高" show-icon :closable="false"></el-alert>
  </nm-tmpl>
</template>
<script>
import page from './page.js'
import attrs from './data/attrs.js'
import slots from './data/slots.js'
export default {
  name: page.name,
  data () {
    return {
      val: '',
      tmpl: {
        title: '盒子栅格化(nm-box-row)',
        icon: 'card',
        attrs,
        slots
      }
    }
  },
  created () {
    this.setTabName(this.tmpl.title)
  }
}
</script>
