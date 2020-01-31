<template>
  <nm-form-page v-bind="form" @success="onSuccess">
    <el-row :gutter="20">
      <el-col :span="5" :offset="1">
        <el-form-item label="权限验证：" prop="validate">
          <el-switch v-model="form.model.validate" />
        </el-form-item>
      </el-col>
      <el-col :span="5" :offset="1">
        <el-form-item label="按钮权限：" prop="button">
          <el-switch v-model="form.model.button" />
        </el-form-item>
      </el-col>
      <el-col :span="5">
        <el-form-item label="审计日志：" prop="auditing">
          <el-switch v-model="form.model.auditing" />
        </el-form-item>
      </el-col>
    </el-row>
    <el-row :gutter="20">
      <el-col :span="5" :offset="1">
        <el-form-item label="单账户登录：" prop="singleAccountLogin">
          <el-switch v-model="form.model.singleAccountLogin" />
        </el-form-item>
      </el-col>
      <el-col :span="5" :offset="1">
        <el-form-item label="JWT刷新令牌有效期：" prop="refreshTokenExpires">
          <el-input v-model.number="form.model.refreshTokenExpires">
            <template slot="append">天</template>
          </el-input>
        </el-form-item>
      </el-col>
      <el-col :span="5">
        <el-form-item label="账户默认密码：" prop="defaultPassword">
          <el-input v-model="form.model.defaultPassword" />
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-page>
</template>
<script>
import { mapMutations } from 'vuex'
const api = $api.admin.system
export default {
  data() {
    return {
      form: {
        header: false,
        action: api.updatePermissionConfig,
        model: this.model,
        labelWidth: '200px'
      }
    }
  },
  props: {
    model: Object
  },
  methods: {
    ...mapMutations('app/system', ['setPermissionConfig']),
    onSuccess() {
      this.setBaseConfig(this.form.model)
    }
  }
}
</script>
