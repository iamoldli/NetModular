<template>
  <nm-form-dialog
    ref="form"
    title="修改密码"
    class="nm-update-password"
    width="400px"
    icon="password"
    :model="model"
    :rules="rules"
    :action="updatePassword"
    :visible.sync="visible_"
    success-msg-text="修改成功"
    @success="success"
  >
    <el-alert title="提示" type="warning" description="密码修改成功后，需要重新登录" show-icon :closable="false"></el-alert>
    <el-row :gutter="20">
      <el-col :span="18" :offset="2">
        <el-form-item label="原密码：" prop="oldPassword">
          <el-input type="password" v-model="model.oldPassword"></el-input>
        </el-form-item>
        <el-form-item label="新密码：" prop="newPassword">
          <el-input type="password" v-model="model.newPassword"></el-input>
        </el-form-item>
        <el-form-item label="确认密码：" prop="confirmPassword">
          <el-input type="password" v-model="model.confirmPassword"></el-input>
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-dialog>
</template>
<script>
import dialog from '../../mixins/components/dialog.js'
import { mapState, mapActions } from 'vuex'

export default {
  name: 'UpdatePassword',
  mixins: [dialog],
  data () {
    // 密码验证
    const validator = (rule, value, callback) => {
      if (value === '') {
        callback(new Error('请输入确认密码'))
      } else if (value !== this.model.newPassword) {
        callback(new Error('两次输入密码不一致!'))
      } else {
        callback()
      }
    }
    return {
      model: {
        oldPassword: '',
        newPassword: '',
        confirmPassword: ''
      },
      rules: {
        oldPassword: [
          { required: true, message: '请输入原密码' }
        ],
        newPassword: [
          { required: true, message: '请输入新密码' },
          { min: 6, message: '密码长度不能小于6' }
        ],
        confirmPassword: [{ validator }]
      }
    }
  },
  computed: {
    ...mapState('app/system', ['updatePassword'])
  },
  methods: {
    ...mapActions('app/system', ['logout']),
    success () {
      this.logout()
    }
  }
}
</script>
<style lang="scss" scoped>
.nm-update-password {
  .el-alert {
    margin-top: -5px;
    margin-bottom: 10px;
    line-height: 15px;
  }
  .el-visible__footer {
    line-height: 15px;
  }
}
</style>
