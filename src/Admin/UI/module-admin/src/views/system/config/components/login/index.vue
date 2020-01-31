<template>
  <nm-form-page v-bind="form" @success="onSuccess">
    <el-row :gutter="20">
      <el-col :span="10" :offset="1">
        <el-form-item label="登录页：" prop="type">
          <el-select v-model="form.model.type">
            <el-option v-for="item in typeOptions" :key="item" :label="item" :value="item"></el-option>
          </el-select>
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="启用验证码：" prop="verifyCode">
          <el-switch v-model="form.model.verifyCode" />
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-page>
</template>
<script>
import { mapState, mapMutations } from 'vuex'
const api = $api.admin.system
export default {
  data() {
    return {
      form: {
        header: false,
        action: api.updateLoginConfig,
        model: this.model
      }
    }
  },
  props: {
    model: Object
  },
  computed: {
    ...mapState('app/system', { typeOptions: s => s.config.login.typeOptions })
  },
  methods: {
    ...mapMutations('app/system', ['setLoginConfig']),
    onSuccess() {
      this.setLoginConfig(this.form.model)
    }
  }
}
</script>
