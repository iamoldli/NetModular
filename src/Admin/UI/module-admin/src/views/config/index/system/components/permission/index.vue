<template>
  <nm-form-page v-bind="form" @success="onSuccess">
    <el-row :gutter="20">
      <el-col :span="10" :offset="1">
        <el-form-item label="权限验证：" prop="validate">
          <el-switch v-model="form.model.validate" />
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="按钮验证：" prop="button">
          <el-switch v-model="form.model.button" />
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
