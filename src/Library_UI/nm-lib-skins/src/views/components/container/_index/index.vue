<template>
  <nm-tmpl v-bind="tmpl">
    <el-alert title="说明" type="warning" :description="desc" show-icon :closable="false"></el-alert>
  </nm-tmpl>
</template>
<script>
import page from './page.js'
import attrs from './data/attrs.js'
export default {
  name: page.name,
  data () {
    return {
      desc: '所有页面级组件都要包裹在 nm-container 容器里面，而容器仅能用于包裹在页面级组件的外面，容器自动填充满整个内容区域，如果启用了滚动条，当容器内页面高度超出时，会显示滚动条',
      tmpl: {
        title: '容器(nm-container)',
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
