<template>
  <div class="nm-login nm-login-container">
    <div class="nm-login-bg"/>
    <div class="nm-login-box">
      <div class="nm-login-content">
        <div class="nm-login-logo">
          <img class="nm-login-logo-img" :src="logo">
          <h1 class="nm-login-logo-title">{{title}}</h1>
        </div>
        <el-form ref="form" :model="form" :rules="rules">
          <el-form-item prop="userName">
            <el-input v-model="form.userName" placeholder="用户名">
              <nm-icon v-slot:prefix name="user"></nm-icon>
            </el-input>
          </el-form-item>
          <el-form-item prop="password">
            <el-input type="password" v-model="form.password" autocomplete="off" placeholder="密码">
              <nm-icon v-slot:prefix name="password"></nm-icon>
            </el-input>
          </el-form-item>
          <el-row :gutter="20">
            <el-col :span="15">
              <el-form-item prop="code">
                <el-input v-model="form.code" autocomplete="off" placeholder="验证码">
                  <nm-icon v-slot:prefix name="verifycode"></nm-icon>
                </el-input>
              </el-form-item>
            </el-col>
            <el-col :span="5">
              <img class="verifycode" title="点击刷新" :src="verifyCodeUrl" @click="refreshVierifyCode">
            </el-col>
          </el-row>
          <el-form-item style="text-align:right;">
            <el-button :loading="loading" class="btnLogin" type="primary" @click="tryLogin">登录</el-button>
          </el-form-item>
        </el-form>
      </div>
    </div>
  </div>
</template>
<script>
import { mapState } from 'vuex'
export default {
  data () {
    return {
      verifyCodeUrl: '',
      form: {
        userName: '',
        password: '',
        code: '',
        pictureId: ''
      },
      rules: {
        userName: [{
          required: true,
          message: '请输入用户名',
          trigger: 'blur'
        }],
        password: [{
          required: true,
          message: '请输入密码',
          trigger: 'blur'
        }],
        code: [{
          required: true,
          message: '请输入验证码',
          trigger: 'blur'
        }]
      },
      loading: false
    }
  },
  props: {
    actions: Object
  },
  computed: {
    ...mapState('app/system', ['title', 'logo']),
    getVerifyCode () {
      return this.actions.getVerifyCode
    },
    login () {
      return this.actions.login
    }
  },
  mounted () {
    this.refreshVierifyCode()
    document.addEventListener('keydown', e => {
      if (e.keyCode === 13) {
        this.tryLogin()
      }
    })
  },
  methods: {
    // 刷新验证码
    async refreshVierifyCode () {
      let data = await this.getVerifyCode()
      this.verifyCodeUrl = data.base64String
      this.form.pictureId = data.id
    },
    // 登录
    tryLogin () {
      this.$refs.form.validate(async valid => {
        if (valid) {
          this.loading = true
          this.login(this.form).then(data => {
            this.loading = false
            // 跳转
            let redirect = this.$route.query.redirect
            if (!redirect || redirect === '') {
              redirect = '/'
            }

            this.$router.push({
              path: redirect
            })
          }).catch(() => {
            this.loading = false
          })
        } else {
          return false
        }
      })
    }
  }
}
</script>
<style lang="scss" scoped>
.nm-login {
  &-container {
    position: absolute;
    padding: 0;
    height: 100%;
    width: 100%;
  }

  &-bg {
    position: absolute;
    top: 0;
    bottom: 0;
    left: 0;
    right: 0;
    opacity: 0.55;
    background-image: url("./img/bg.jpg");
    background-position: center center;
    background-repeat: no-repeat;
    background-attachment: fixed;
    background-size: cover;
    z-index: -1;
  }

  &-box {
    margin: 0 auto;
    margin-top: 200px;
    text-align: center;

    .btnLogin {
      margin-top: 10px;
      width: 100%;
    }
  }

  &-content {
    padding: 0 15px;
    border-radius: 5px;
    display: inline-block;
    background: rgba(255, 255, 255, 0.8);

    .verifycode {
      cursor: pointer;
    }
  }

  &-logo {
    padding: 10px 0;

    &-img {
      padding: 5px;
      box-sizing: border-box;
      height: 80px;
      float: left;
    }

    &-title {
      margin: 0;
      padding: 0;
      height: 80px;
      line-height: 80px;
      display: inline-block;
    }
  }

  .icon {
    font-size: 1.2rem;
  }
}
</style>
