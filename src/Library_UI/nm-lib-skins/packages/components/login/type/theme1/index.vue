<template>
  <div class="anim_fade">
    <div class="nm-login nm-login-container theme1">
      <div class="nm-login-bg"/>
      <div class="nm-login-box">
        <div class="nm-login-content">
          <div class="nm-login-top">
            <div class="nm-login-logo">
              <img class="nm-login-logo-img" :src="logo" style="height:60px;width:60px">
              <h2 class="nm-login-logo-title">{{title}}</h2>
            </div>
          </div>
          <div class="nm-login-foot">
            <el-form ref="form" :model="form">
              <el-form-item prop="userName">
                <el-input v-model="form.userName" placeholder="用户名">
                  <template v-slot:prepend>
                    <nm-icon v-slot:prefix name="user"></nm-icon>
                  </template>
                </el-input>
                <span v-show="form.userName" class="el-icon-check login-success"></span>
                <span v-show="!form.userName && showErr" class="el-icon-close login-error"></span>
              </el-form-item>
              <el-form-item prop="password">
                <el-input type="password" v-model="form.password" autocomplete="off" placeholder="密码">
                  <template v-slot:prepend>
                    <nm-icon v-slot:prefix name="password"></nm-icon>
                  </template>
                </el-input>
                <span v-show="form.password" class="el-icon-check login-success"></span>
                <span v-show="!form.password && showErr" class="el-icon-close login-error"></span>
              </el-form-item>
              <div class="form-group">
                <el-form-item prop="code" style="float: left;width:212px;">
                  <el-input v-model="form.code" autocomplete="off" placeholder="验证码">
                    <template v-slot:prepend>
                      <nm-icon v-slot:prefix name="verifycode"></nm-icon>
                    </template>
                  </el-input>
                  <span v-show="form.code" class="el-icon-check login-success"></span>
                  <span v-show="!form.code && showErr" class="el-icon-close login-error"></span>
                </el-form-item>

                <div class="nm-login-verifycode">
                  <div class="el-icon-loading nm-login-verifycodeLoading" v-show="!verifycodeShow"></div>
                  <img
                    class="verifycode"
                    title="点击刷新"
                    :class="!verifycodeShow?'anim_fade':''"
                    :style="!verifyCodeUrl?'opacity: 0':''"
                    :src="verifyCodeUrl"
                    @click="refreshVierifyCode"
                  >
                </div>
              </div>
              <el-form-item>
                <el-button-group>
                  <el-button :loading="loading" class="btnLogin" type="primary" @click="tryLogin" style="width:80px;">登录</el-button>
                  <el-button class="btnLogin" type="danger" @click="reset" style="width:80px;">重置</el-button>
                </el-button-group>
              </el-form-item>
            </el-form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import { mapState } from 'vuex'
export default {
  data() {
    return {
      verifycodeShow: false,
      verifyCodeUrl: '',
      form: {
        userName: '',
        password: '',
        code: '',
        pictureId: ''
      },
      loading: false,
      showErr: false
    }
  },
  props: {
    actions: Object
  },
  computed: {
    ...mapState('app/system', ['title', 'logo']),
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
      this.verifycodeShow = false
      let data = await this.getVerifyCode()

      setTimeout(() => {
        this.verifycodeShow = true
        this.verifyCodeUrl = data.base64String
        this.form.pictureId = data.id
      }, 100)
    },
    // 登录
    tryLogin() {
      if (this.form.userName && this.form.password && this.form.password) {
        this.$refs['form'].validate(async valid => {
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
      } else {
        this.showErr = true
      }
    },
    reset() {
      this.form.userName = null
      this.form.password = null
      this.form.code = null
    }
  }
}
</script>
<style lang="scss">
$lonin-error: #ff0606;
$lonin-success: #67c23a;
$lonin-info: #409eff;
.login-success {
  color: #00a65a;
  font-size: 18px;
  font-weight: 1000 !important;
  position: absolute;
  top: 10px;
  right: 10px;
}
.login-error {
  color: $lonin-error;
  font-size: 14px;
  font-weight: 1000 !important;
  position: absolute;
  top: 13px;
  right: 10px;
}
.nm-login.theme1 {
  &-container {
    padding: 0;
    height: 100%;
    width: 100%;
  }
  &-verifycodeLoading {
    position: absolute;
    margin-top: 15px;
    margin-left: 40px;
    z-index: 100;
  }
  &-verifycode {
    height: 40px;
    background-color: #fff;
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

    .verifycode {
      cursor: pointer;
    }
  }

  &-logo {
    padding: 10px 0;
    &-img {
      padding: 5px;
      box-sizing: border-box;
      height: 60px;
      float: left;
    }

    &-title {
      margin: 0;
      padding: 0;
      height: 60px;
      line-height: 60px;
      display: inline-block;
    }
  }

  .nm-icon {
    font-size: 1.2rem;
    color: rgba(0, 0, 0, 0.6);
  }

  &-top {
    padding: 5px;
    border-radius: 5px 5px 0px 0px;
    background: rgba(0, 0, 0, 0.6);
    color: #ffffff;
    width: 400px;
  }
  &-foot {
    padding: 5px 5px 1px 5px;
    border-radius: 0px 0px 5px 5px;
    background: rgba(255, 250, 2550, 0.6);
    width: 400px;
    .el-input-group__prepend {
      padding: 0 10px !important;
    }
    .el-form {
      margin: 20px 40px 20px 40px;
    }
  }
}
@keyframes fadeIn {
  0% {
    opacity: 0;
  }
  25% {
    opacity: 0.25;
  }
  50% {
    opacity: 0.5;
  }
  75% {
    opacity: 0.75;
  }
  100% {
    opacity: 1;
  }
}
.anim_fade {
  animation: fadeIn ease-in 1s;
  animation-fill-mode: forwards;
}
</style>
