<template>
  <nm-container>
    <nm-tabs>
      <el-tabs v-model="tab">
        <el-tab-pane name="system" lazy>
          <span slot="label"> <nm-icon name="system"></nm-icon>系统配置 </span>
          <system-pane ref="system" />
        </el-tab-pane>
        <el-tab-pane name="module" lazy>
          <span slot="label"> <nm-icon name="app"></nm-icon>模块配置 </span>
          <module-pane ref="module" />
        </el-tab-pane>
        <el-tab-pane name="custom" lazy>
          <span slot="label"> <nm-icon name="list"></nm-icon>自定义配置 </span>
          <custom-pane ref="custom" />
        </el-tab-pane>
      </el-tabs>
    </nm-tabs>
  </nm-container>
</template>
<script>
import page from './page'
import SystemPane from './system'
import ModulePane from './module'
import CustomPane from './custom'
export default {
  name: page.name,
  components: { SystemPane, ModulePane, CustomPane },
  data() {
    return {
      tab: 'system'
    }
  }
}
</script>
