<template>
  <section>
    <nm-demo-desc>
      <p>
        添加
        <code>fullscreen</code>属性，可以开启全屏功能
      </p>
    </nm-demo-desc>
    <nm-button type="success" @click="visible=true">打开</nm-button>
    <nm-dialog title="可全屏的对话框" :visible.sync="visible" fullscreen>
      <h1>可全屏的对话框</h1>
    </nm-dialog>
  </section>
</template>
<script>
export default {
  data () {
    return {
      visible: false
    }
  }
}
</script>
