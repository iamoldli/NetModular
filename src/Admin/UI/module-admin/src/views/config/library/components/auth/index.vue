<template>
  <nm-form-page v-bind="form" @success="onSuccess">
    <el-divider content-position="left">认证&授权</el-divider>
    <el-row>
      <el-col :span="5" :offset="1">
        <el-form-item label="验证码：" prop="verifyCode">
          <el-switch v-model="form.model.verifyCode" />
        </el-form-item>
      </el-col>
      <el-col :span="5">
        <el-form-item label="权限验证：" prop="validate">
          <el-switch v-model="form.model.validate" />
        </el-form-item>
      </el-col>
      <el-col :span="5">
        <el-form-item label="按钮验证：" prop="button">
          <el-switch v-model="form.model.button" />
        </el-form-item>
      </el-col>
      <el-col :span="5">
        <el-form-item label="单账户登录：" prop="singleAccount">
          <el-switch v-model="form.model.singleAccount" />
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="5" :offset="1">
        <el-form-item label="审计日志：" prop="auditing">
          <el-switch v-model="form.model.auditing" />
        </el-form-item>
      </el-col>
    </el-row>
    <el-divider content-position="left">JWT参数</el-divider>
    <el-row>
      <el-col :span="10" :offset="1">
        <el-form-item label="密钥(Key)：" prop="jwt.key">
          <el-input v-model="form.model.jwt.key" clearable />
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="有效期(Expires)：" prop="jwt.expires">
          <el-input v-model.number="form.model.jwt.expires">
            <template slot="append">分钟</template>
          </el-input>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="10" :offset="1">
        <el-form-item label="发行人(Issuer)：" prop="jwt.issuer">
          <el-input v-model="form.model.jwt.issuer" clearable />
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="消费者(Audience)：" prop="jwt.audience">
          <el-input v-model="form.model.jwt.audience" clearable />
        </el-form-item>
      </el-col>
    </el-row>
    <el-divider content-position="left">登录方式</el-divider>
    <el-row>
      <el-col :span="5" :offset="1">
        <el-form-item label="用户名：">
          <el-switch v-model="form.model.loginMode.userName" />
        </el-form-item>
      </el-col>
      <el-col :span="5">
        <el-form-item label="邮箱：">
          <el-switch v-model="form.model.loginMode.email" />
        </el-form-item>
      </el-col>
      <el-col :span="5">
        <el-form-item label="用户名或邮箱：">
          <el-switch v-model="form.model.loginMode.userNameOrEmail" />
        </el-form-item>
      </el-col>
      <el-col :span="5">
        <el-form-item label="手机号：">
          <el-switch v-model="form.model.loginMode.phone" />
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="5" :offset="1">
        <el-form-item label="微信扫码登录：">
          <el-switch v-model="form.model.loginMode.weChatScanCode" disabled />
        </el-form-item>
      </el-col>
      <el-col :span="5">
        <el-form-item label="QQ：">
          <el-switch v-model="form.model.loginMode.qq" disabled />
        </el-form-item>
      </el-col>
      <el-col :span="5">
        <el-form-item label="GitHub：">
          <el-switch v-model="form.model.loginMode.gitHub" disabled />
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
      code: 'Auth',
      form: {
        labelWidth: '200px',
        model: {
          verifyCode: false,
          validate: false,
          button: false,
          singleAccount: false,
          auditing: false,
          jwt: {
            key: '',
            issuer: '',
            audience: '',
            expires: 120,
            refreshTokenExpires: 7
          },
          loginMode: {
            userName: true,
            email: false,
            userNameOrEmail: false,
            phone: false,
            weChatScanCode: false,
            qq: false,
            gitHub: false
          }
        },
        rules: {
          'jwt.key': [
            { required: true, message: '请填写JWT的key' },
            {
              type: 'string',
              len: 16,
              message: '密钥长度必须为16位'
            }
          ],
          'jwt.expires': [
            { required: true, message: '请填写令牌有效期' },
            { type: 'number', min: 1, message: '有效期不能小于1分钟' }
          ]
        }
      }
    }
  },
  methods: {
    onSuccess() {
      this.setUIConfig({
        permission: {
          validate: this.form.model.validate,
          button: this.form.model.button
        },
        component: {
          login: { verifyCode: this.form.model.verifyCode }
        }
      })
    }
  }
}
</script>
