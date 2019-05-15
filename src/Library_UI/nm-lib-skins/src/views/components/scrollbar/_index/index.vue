<template>
  <nm-tmpl v-bind="tmpl">
    <el-alert title="注意事项" type="warning" :closable="false">滚动条的父元素必须设置高度</el-alert>
  </nm-tmpl>
</template>
<script>
import page from './page.js'
import attrs from './data/attrs.js'
import methods from './data/methods.js'
export default {
  name: page.name,
  data() {
    return {
      val: '',
      tmpl: {
        title: '滚动条(nm-scrollbar)',
        icon: 'list',
        attrs,
        methods
      }
    }
  },
  created() {
    this.setTabName(this.tmpl.title)
  }
}
</script>
