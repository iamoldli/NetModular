<template>
  <nm-form-page v-bind="form">
    <el-form-item label="选择模块：" prop="module">
      <nm-module-select v-model="form.model.module" />
    </el-form-item>
  </nm-form-page>
</template>
<script>
export default {
  data() {
    return {
      form: {
        header: false,
        model: {
          module: ''
        }
      }
    }
  }
}
</script>
<style lang="scss">
.nm-admin-config-module {
  .el-tab-pane {
    padding: 10px;
  }
}
</style>
