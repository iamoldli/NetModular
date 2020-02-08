<template>
  <el-tabs class="nm-admin-config-module" v-model="tab" tab-position="left" type="border-card">
    <el-tab-pane v-for="item in list" :key="item.value" :name="item.value">
      <span slot="label"><nm-icon :name="item.data"></nm-icon>{{ item.label }} </span>
      <form-page :module-code="item.value" />
    </el-tab-pane>
  </el-tabs>
</template>
<script>
import FormPage from './form'
const { select } = $api.admin.moduleInfo
export default {
  components: { FormPage },
  data() {
    return {
      tab: '',
      list: []
    }
  },
  created() {
    select().then(data => {
      this.list = data
      if (this.list.length > 0) this.tab = this.list[0].value
    })
  }
}
</script>

<style lang="scss">
.nm-admin-config-module {
  height: 100%;

  .el-tabs__header {
    margin-right: 0 !important;
  }

  .el-tabs__item.is-active {
    color: #67c23a !important;
  }
  .el-tabs__content {
    .nm-box {
      border-left: none !important;
    }
  }
}
</style>
