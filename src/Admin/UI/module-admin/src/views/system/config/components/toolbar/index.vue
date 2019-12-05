<template>
  <nm-form-page v-bind="form" @success="onSuccess">
    <el-row :gutter="20">
      <el-col :span="5" :offset="1">
        <el-form-item label="全屏控制：" prop="fullscreen">
          <el-switch v-model="form.model.fullscreen" />
        </el-form-item>
      </el-col>
      <el-col :span="5">
        <el-form-item label="皮肤设置：" prop="skin">
          <el-switch v-model="form.model.skin" />
        </el-form-item>
      </el-col>
      <el-col :span="5">
        <el-form-item label="退出按钮：" prop="logout">
          <el-switch v-model="form.model.logout" />
        </el-form-item>
      </el-col>
      <el-col :span="5">
        <el-form-item label="用户信息：" prop="userInfo">
          <el-switch v-model="form.model.userInfo" />
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
        action: api.updateToolbarConfig,
        model: this.model
      }
    }
  },
  props: {
    model: Object
  },
  methods: {
    ...mapMutations('app/system', ['setToolbarConfig']),
    onSuccess() {
      this.setToolbarConfig(this.form.model)
    }
  },
  watch: {
    model() {
      this.form.model = this.model
    }
  }
}
</script>
