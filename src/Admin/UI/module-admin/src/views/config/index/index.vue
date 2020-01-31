<template>
  <nm-container>
    <nm-tabs>
      <el-tabs v-model="tab">
        <el-tab-pane name="module">
          <span slot="label"> <nm-icon name="app"></nm-icon>模块配置 </span>
          <module-tab ref="module" />
        </el-tab-pane>
        <el-tab-pane name="list">
          <span slot="label"> <nm-icon name="list"></nm-icon>全部配置 </span>
          <list-tab ref="list" />
        </el-tab-pane>
      </el-tabs>
    </nm-tabs>
  </nm-container>
</template>
<script>
import ListTab from './list'
import ModuleTab from './module'
export default {
  components: { ListTab, ModuleTab },
  data() {
    return {
      tab: 'module'
    }
  }
}
</script>
