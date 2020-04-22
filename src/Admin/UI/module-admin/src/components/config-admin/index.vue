<template>
  <nm-form-page v-bind="form">
    <el-row>
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
import module from '../../module'
const { edit, update } = $api.admin.config
export default {
  data() {
    return {
      code: module.code,
      type: 1,
      form: {
        header: false,
        action: this.update,
        labelWidth: '200px',
        model: {
          auditing: false,
          defaultPassword: ''
        }
      }
    }
  },
  methods: {
    update() {
      return update({ type: this.type, code: this.code, json: JSON.stringify(this.form.model) })
    }
  },
  created() {
    edit({ type: this.type, code: this.code }).then(data => {
      this.form.model = data
    })
  }
}
</script>
