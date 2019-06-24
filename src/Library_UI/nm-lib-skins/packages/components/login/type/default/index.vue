<template>
  <div class="nm-login nm-login-container default">
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
              <template v-slot:prefix>
                <nm-icon name="user"></nm-icon>
              </template>
            </el-input>
          </el-form-item>
          <el-form-item prop="password">
            <el-input type="password" v-model="form.password" autocomplete="off" placeholder="密码">
              <template v-slot:prefix>
                <nm-icon name="password"></nm-icon>
              </template>
            </el-input>
          </el-form-item>
          <div v-if="loginVerifyCode" class="verifycode">
            <div class="verifycode-input">
              <el-form-item prop="code">
                <el-input v-model="form.code" autocomplete="off" placeholder="验证码">
                  <template v-slot:prefix>
                    <nm-icon name="verifycode"></nm-icon>
                  </template>
                </el-input>
              </el-form-item>
            </div>
            <div class="verifycode-img">
              <img title="点击刷新" :src="verifyCodeUrl" @click="refreshVierifyCode">
            </div>
          </div>
          <el-form-item style="text-align:right;">
            <el-button :loading="loading" class="btn-login" type="primary" @click="tryLogin">登录</el-button>
          </el-form-item>
        </el-form>
      </div>
    </div>
  </div>
</template>
<script>
import { mapState } from 'vuex'
export default {
  data() {
    const _this = this
    return {
      verifyCodeUrl: '',
      form: {
        userName: 'admin',
        password: 'admin',
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
        code: [{ validator(rule, value, callback) {
          if (_this.loginVerifyCode && value === '') {
            callback(new Error('请输入验证码'))
          } else {
            callback()
          }
        },
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
    ...mapState('app/system', ['title', 'logo', 'loginVerifyCode']),
    getVerifyCode() {
      return this.actions.getVerifyCode
    },
    login() {
      return this.actions.login
    }
  },
  mounted() {
    this.refreshVierifyCode()
    document.addEventListener('keydown', e => {
      if (e.keyCode === 13) {
        this.tryLogin()
      }
    })
  },
  methods: {
    // 刷新验证码
    async refreshVierifyCode() {
      let data = await this.getVerifyCode()
      this.verifyCodeUrl = data.base64String
      this.form.pictureId = data.id
    },
    // 登录
    tryLogin() {
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
<style lang="scss">
.nm-login.default {
  .nm-login-container {
    position: absolute;
    padding: 0;
    height: 100%;
    width: 100%;
  }

  .nm-login-bg {
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

  .nm-login-box {
    margin: 0 auto;
    padding: 5px 15px;
    margin-top: 10%;
    border-radius: 5px;
    text-align: center;
    width: 350px;
    background: rgba(255, 255, 255, 0.7);
  }

  .nm-login-content {
    .verifycode {
      display: flex;
      flex-direction: row;
      align-items: stretch;

      &-input {
        padding-right: 10px;
        flex-grow: 1;
      }

      &-img {
        flex-shrink: 0;
        img {
          margin-top: 1px;
          height: 38px;
          cursor: pointer;
        }
      }
    }
    .btn-login {
      width: 100%;
      cursor: pointer;
    }
  }

  .nm-login-logo {
    position: relative;
    padding: 10px 0 10px 85px;

    &-img {
      position: absolute;
      left: 5px;
      top: 20px;
      padding: 5px;
      -webkit-box-sizing: border-box;
      box-sizing: border-box;
      height: 61px;
      float: left;
      background: #409eff;
    }

    &-title {
      margin: 0;
      padding: 0;
      height: 80px;
      line-height: 80px;
      font-size: 33px;
      text-align: left;
    }
  }

  .nm-icon {
    padding-top: 5px;
    font-size: 2em;
  }
  .el-input__inner {
    padding-left: 35px !important;
  }
}
</style>
