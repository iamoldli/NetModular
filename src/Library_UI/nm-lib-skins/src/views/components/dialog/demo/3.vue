<template>
  <section>
    <nm-demo-desc>
      <p>
        通过
        <code>footer</code>插槽，可以在底部添加自定义内容
      </p>
    </nm-demo-desc>
    <nm-button type="success" @click="visible=true">打开</nm-button>
    <nm-dialog ref="myDialog" title="自定义底部按钮" :visible.sync="visible" fullscreen>
      <h1>自定义底部按钮</h1>
      <template v-slot:footer>
        <nm-button type="success">确定</nm-button>
        <nm-button type="info" @click="close">关闭</nm-button>
      </template>
    </nm-dialog>
  </section>
</template>
<script>
export default {
  data () {
    return {
      visible: false
    }
  },
  methods: {
    close () {
      this.$refs.myDialog.close()
    }
  }
}
</script>
