<template>
  <section>
    <nm-demo-desc>
      <p>监听事件</p>
    </nm-demo-desc>
    <nm-button type="success" @click="visible=true">打开</nm-button>
    <nm-dialog
      ref="myDialog"
      title="监听事件"
      :visible.sync="visible"
      @open="_success('打开了')"
      @close="_warning('关闭了')"
    >
      <h1>监听事件</h1>
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
