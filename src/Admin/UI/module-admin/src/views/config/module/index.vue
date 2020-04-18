<template>
  <el-tabs class="nm-admin-config-module page" v-model="tab" tab-position="left" type="border-card">
    <el-tab-pane v-for="item in list" :key="item.value" :name="item.value" lazy>
      <span slot="label"><nm-icon :name="item.data.icon"></nm-icon>{{ item.label }} </span>
      <component :is="`nm-config-${item.value.toLowerCase()}`"></component>
    </el-tab-pane>
  </el-tabs>
</template>
<script>
const { select } = $api.admin.module
export default {
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
  .el-tabs__header {
    margin-right: 0 !important;
  }
  .el-tabs__item {
    text-align: left !important;

    &.is-active {
      color: #67c23a !important;
    }
  }
  .el-tabs__content {
    .nm-box {
      border-left: none !important;
    }
  }
}
</style>
