<template>
  <nm-drawer v-bind="drawer" :visible.sync="visible_">
    <el-form label-width="120px" disabled>
      <el-form-item label="账户：">{{ model.creator }}</el-form-item>
      <el-form-item label="模块：">{{ model.moduleName }}({{ model.area }})</el-form-item>
      <el-form-item label="控制器：">{{ model.controller }}({{ model.controllerDesc }})</el-form-item>
      <el-form-item label="方法：">{{ model.action }}({{ model.actionDesc }})</el-form-item>
      <el-form-item label="执行时间：">{{ model.executionTime }}</el-form-item>
      <el-form-item label="用时：">{{ model.executionDuration }}ms</el-form-item>
      <el-form-item label="IP：">{{ model.ip }}</el-form-item>
      <el-form-item label="平台：">{{ model.platformName }}</el-form-item>
      <el-form-item label="浏览器信息：">
        <el-input type="textarea" :value="model.browserInfo" :rows="5" />
      </el-form-item>
      <el-form-item label="参数：">
        <el-input type="textarea" :value="model.parameters" :rows="10" />
      </el-form-item>
      <el-form-item label="结果：">
        <el-input type="textarea" :value="model.result" :rows="10" />
      </el-form-item>
    </el-form>
  </nm-drawer>
</template>
<script>
import { mixins } from 'netmodular-ui'

// 接口
const api = $api.admin.auditInfo

export default {
  mixins: [mixins.drawer],
  data() {
    return {
      drawer: {
        header: true,
        title: '审计日志详情',
        icon: 'log',
        width: '600px',
        draggable: true
      },
      model: {}
    }
  },
  props: ['id'],
  methods: {
    get() {
      if (this.id === '') {
        this._warning('请选择要查看的数据~')
        return
      }
      api.details(this.id).then(data => {
        this.model = data
      })
    }
  },
  watch: {
    id() {
      if (this.id > 0) {
        this.get()
      }
    }
  }
}
</script>
