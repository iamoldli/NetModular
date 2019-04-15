<template>
  <nm-container>
    <nm-box page header title="盒子(nm-box)" icon="app">
      <nm-demo-desc>
        <p>容器比较简单，主要用于内容主体的布局，所有frame中的页面，都应该包裹在容器中</p>
      </nm-demo-desc>
      <nm-table-attributes :data="attrs"/>
      <nm-demo :options="demo1">
        <div style="height:200px;position:relative">
          <nm-container>
            <p>容器比较简单，主要用于内容主体的布局，所有frame中的页面，都应该包裹在容器中</p>
          </nm-container>
        </div>
      </nm-demo>
    </nm-box>
  </nm-container>
</template>
<script>
import attrs from './data/attrs.js'
export default {
  data () {
    return {
      attrs,
      demo1: {
        html: `<nm-container>
  <p>容器比较简单，主要用于内容主体的布局，所有frame中的页面，都应该包裹在容器中</p>
</nm-container>`
      }
    }
  }
}
</script>
