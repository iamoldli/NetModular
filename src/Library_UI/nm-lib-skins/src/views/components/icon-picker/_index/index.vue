<template>
  <nm-tmpl v-bind="tmpl">
    <el-alert title="说明" type="warning" show-icon :closable="false">
      所有图标均使用svg格式，从
      <a href="http://iconfont.cn" target="_blank">阿里巴巴图标库</a> 获取
    </el-alert>
  </nm-tmpl>
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
        title: '图标选择器(nm-icon-picker)',
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
