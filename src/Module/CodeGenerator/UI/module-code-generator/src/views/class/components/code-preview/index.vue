<template>
  <nm-dialog v-bind="dialog" :visible.sync="visible_">
    <el-tabs type="border-card">
      <el-tab-pane label="用户管理">用户管理</el-tab-pane>
      <el-tab-pane label="配置管理">配置管理</el-tab-pane>
      <el-tab-pane label="角色管理">角色管理</el-tab-pane>
      <el-tab-pane label="定时任务补偿">定时任务补偿</el-tab-pane>
    </el-tabs>
  </nm-dialog>
</template>
<script>
import { mixins } from 'nm-lib-skins'
export default {
  mixins: [mixins.dialog],
  data() {
    return {
      dialog: {
        title: '代码预览',
        icon: 'develop',
        height: '80%'
      }
    }
  },
  props: ['id']
}
</script>
