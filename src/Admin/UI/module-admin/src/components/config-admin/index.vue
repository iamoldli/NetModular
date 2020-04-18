<template>
  <nm-form-page v-bind="form">
    <el-row :gutter="20">
      <el-col :span="10" :offset="1">
        <el-form-item label="审计日志：" prop="auditing">
          <el-switch v-model="form.model.auditing" />
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="账户默认密码：" prop="defaultPassword">
          <el-input v-model="form.model.defaultPassword" />
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-page>
</template>
<script>
import mixins from '../../mixins/config-form'
import module from '../../module'
export default {
  mixins: [mixins],
  data() {
    return {
      code: module.code,
      form: {
        labelWidth: '200px',
        model: {
          auditing: false,
          defaultPassword: ''
        }
      }
    }
  }
}
</script>
