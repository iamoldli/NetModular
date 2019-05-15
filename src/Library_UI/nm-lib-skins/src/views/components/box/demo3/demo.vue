<template>
  <nm-box v-bind="box">
    <el-alert title="全屏按钮" type="success" description="通过 fullscreen 属性可以开启全屏功能" show-icon :closable="false"></el-alert>
    <el-alert title="折叠按钮" type="success" description="通过 collapse 属性可以开启折叠功能" show-icon :closable="false"></el-alert>
    <el-alert title="自定义工具栏" type="warning" description="通过 toolbar 插槽可以实现自定义工具栏" show-icon :closable="false"></el-alert>

    <template v-slot:toolbar>
      <nm-button icon="system" @click="onClick"/>
    </template>
  </nm-box>
</template>
<script>
export default {
  data() {
    return {
      box: {
        header: true,
        title: '工具栏示例',
        icon: 'app',
        fullscreen: true,
        collapse: true
      }
    }
  },
  methods: {
    onClick() {
      this._success('我是自定义工具栏按钮')
    }
  }
}
</script>
