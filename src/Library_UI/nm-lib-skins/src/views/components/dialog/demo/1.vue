<template>
  <section>
    <nm-button type="success" @click="visible=true">打开</nm-button>
    <nm-dialog title="简单对话框" icon="list" draggable no-footer :visible.sync="visible" fullscreen>
      <h1>哈哈哈哈哈哈哈哈哈</h1>
      <nm-button text="显示" @click="show=!show"/>
      <el-input v-if="show"/>
    </nm-dialog>
  </section>
</template>
<script>
export default {
  data () {
    return {
      visible: false,
      show: true
    }
  }
}
</script>
