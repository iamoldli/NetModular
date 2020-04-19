<template>
  <nm-form-page v-bind="form">
    <el-row :gutter="20">
      <el-col :span="10" :offset="1">
        <el-form-item label="开启：" prop="enabled">
          <el-switch v-model="form.model.enabled" />
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="日志：" prop="logger">
          <el-switch v-model="form.model.logger" />
        </el-form-item>
      </el-col>
      <el-col :span="10" :offset="1">
        <el-form-item label="实例名称：" prop="instanceName">
          <el-input v-model="form.model.instanceName" />
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="表前缀：" prop="tablePrefix">
          <el-input v-model="form.model.tablePrefix" />
        </el-form-item>
      </el-col>
    </el-row>
    <el-divider content-position="center"><span class="nm-text-warning">Quartz的数据库配置与任务调度模块保持一致</span></el-divider>
    <el-row :gutter="20">
      <el-col :span="10" :offset="1">
        <el-form-item label="数据库类型：">
          <el-select disabled v-model="form.model.provider">
            <el-option label="SqlServer" :value="0"> </el-option>
            <el-option label="MySql" :value="1"> </el-option>
            <el-option label="SQLite" :value="2"> </el-option>
            <el-option label="Oracle" :value="3"> </el-option>
            <el-option label="PostgreSQL" :value="4"> </el-option>
          </el-select>
        </el-form-item>
      </el-col>
      <el-col :span="20" :offset="1">
        <el-form-item label="数据库连接字符串：">
          <el-input disabled v-model="form.model.connectionString" />
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-page>
</template>
<script>
import mixins from '../../form-mixins'
export default {
  mixins: [mixins],
  data() {
    return {
      code: 'Quartz',
      form: {
        labelWidth: '200px',
        model: {
          enabled: false,
          logger: false,
          instanceName: '',
          tablePrefix: '',
          provider: 0,
          connectionString: ''
        },
        rules: {
          instanceName: [{ required: true, message: '实例名称不能为空' }]
        }
      }
    }
  }
}
</script>
