
<template>
  <nm-container style="background-color:#fff">
    <nm-box page header title="分割线(nm-divider)" icon="app">
      <nm-demo-desc>
        <p>区隔内容的分割线。对不同章节的文本段落进行分割。对行内文字/链接进行分割，例如表格的操作列。</p>
        <p>关于分隔线的样式可以自行设置高度、长度、边距、颜色、字体等</p>
      </nm-demo-desc>
      <nm-table-attributes :data="attrs"/>
      <p style="margin-bottom:30px;">1、垂直分割线</p>Text
      <nm-divider type="vertical"/>
      <a href="#">Link</a>
      <nm-divider type="vertical"/>
      <a href="#">Link</a>
      <p style="margin-top:30px;">2、基本功能.</p>
      <nm-divider style="height: 2px;"/>
      <p>3、添加文字并且文字居中</p>
      <nm-divider>red</nm-divider>
      <p>4、虚线</p>
      <nm-divider dashed/>
      <p>5、添加文字并且文字居左</p>
      <nm-divider orientation="left" border-color="red">Left Text</nm-divider>
      <p>6、添加文字并且文字居右</p>
      <nm-divider orientation="right">Right Text</nm-divider>
    </nm-box>
  </nm-container>
</template>
<script>
import attrs from './data/attrs.js'
export default {
  data () {
    return { attrs }
  }
}
</script>
