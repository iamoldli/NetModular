<template>
  <el-tabs class="nm-admin-config-module page" v-model="tab" tab-position="left" type="border-card">
    <el-tab-pane v-for="item in modules" :key="item.name" :name="item.name" lazy>
      <span slot="label"><nm-icon :name="item.icon"></nm-icon>{{ getNo(item) }}_{{ item.label }} </span>
      <component :is="item.name"></component>
    </el-tab-pane>
  </el-tabs>
</template>
<script>
import { mapState } from 'vuex'
const { select } = $api.admin.module
export default {
  data() {
    return {
      tab: '',
      modules: []
    }
  },
  computed: {
    ...mapState('app/system', ['globalComponents'])
  },
  methods: {
    getNo(item) {
      return item.id < 10 ? '0' + item.id : '' + item.id
    }
  },
  created() {
    select().then(data => {
      data.forEach(m => {
        let module = {
          id: m.data.id,
          label: m.label,
          name: `nm-config-${m.value.toLowerCase()}`,
          icon: m.data.icon
        }
        if (this.globalComponents.includes(module.name)) {
          this.modules.push(module)
        }
      })
      if (this.modules.length > 0) this.tab = this.modules[0].name
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
