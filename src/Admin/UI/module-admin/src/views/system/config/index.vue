<template>
  <nm-container>
    <nm-form-page ref="form" v-bind="form" @success="onSuccess">
      <el-divider content-position="left">基础信息</el-divider>
      <el-row :gutter="20">
        <el-col :span="10" :offset="1">
          <el-form-item label="标题：" prop="title">
            <el-input v-model="form.model.title" />
          </el-form-item>
          <el-form-item label="首页：" prop="home">
            <el-input v-model="form.model.home" />
          </el-form-item>
        </el-col>
        <el-col :span="10" :offset="1">
          <el-form-item label="Logo：" prop="logo">
            <el-upload class="logo-uploader" v-bind="logoUpload">
              <img v-if="form.model.logoUrl" :src="form.model.logoUrl" class="logo-uploader-img" />
              <i v-else class="el-icon-plus logo-uploader-icon"></i>
            </el-upload>
          </el-form-item>
        </el-col>
      </el-row>
      <el-row :gutter="20">
        <el-col :span="10" :offset="1">
          <el-form-item label="个人信息页：" prop="userInfoPage">
            <el-input v-model="form.model.userInfoPage" placeholder="路由名称，默认为userinfo" />
          </el-form-item>
        </el-col>
        <el-col :span="10">
          <el-form-item label="版权申明：" prop="copyright">
            <el-input v-model="form.model.copyright" placeholder="" />
          </el-form-item>
        </el-col>
      </el-row>
      <el-divider content-position="left">登录控制</el-divider>
      <el-row :gutter="20">
        <el-col :span="5" :offset="1">
          <el-form-item label="登录页：" prop="type">
            <el-select v-model="form.model.loginOptions.type">
              <el-option v-for="item in loginOptions.typeOptions" :key="item" :label="item" :value="item"></el-option>
            </el-select>
          </el-form-item>
        </el-col>
        <el-col :span="5">
          <el-form-item label="启用验证码：" prop="verifyCode">
            <el-switch v-model="form.model.loginOptions.verifyCode" />
          </el-form-item>
        </el-col>
      </el-row>
      <el-divider content-position="left">组件设置</el-divider>
      <el-row :gutter="20">
        <el-col :span="5" :offset="1">
          <el-form-item labelWidth="160px" label="菜单只能打开一个" prop="menuUniqueOpened">
            <el-switch v-model="form.model.menuUniqueOpened" />
          </el-form-item>
        </el-col>
        <el-col :span="5">
          <el-form-item labelWidth="200px" label="点击模态框可关闭对话框" prop="dialogCloseOnClickModal">
            <el-switch v-model="form.model.dialogCloseOnClickModal" />
          </el-form-item>
        </el-col>
      </el-row>
      <el-divider content-position="left">工具栏</el-divider>
      <el-row :gutter="20">
        <el-col :span="5" :offset="1">
          <el-form-item label="全屏控制：" prop="toolbar.fullscreen">
            <el-switch v-model="form.model.toolbar.fullscreen" />
          </el-form-item>
        </el-col>
        <el-col :span="5">
          <el-form-item label="皮肤设置：" prop="toolbar.skin">
            <el-switch v-model="form.model.toolbar.skin" />
          </el-form-item>
        </el-col>
        <el-col :span="5">
          <el-form-item label="退出按钮：" prop="toolbar.logout">
            <el-switch v-model="form.model.toolbar.logout" />
          </el-form-item>
        </el-col>
        <el-col :span="5">
          <el-form-item label="用户信息：" prop="toolbar.userInfo">
            <el-switch v-model="form.model.toolbar.userInfo" />
          </el-form-item>
        </el-col>
      </el-row>
      <el-divider content-position="left">自定义CSS</el-divider>
      <el-row :gutter="20">
        <el-col :span="24">
          <el-form-item prop="customCss">
            <el-input type="textarea" :rows="5" placeholder="请输入自定义Css" v-model="form.model.customCss"></el-input>
          </el-form-item>
        </el-col>
      </el-row>
      <el-divider content-position="left">开发属性</el-divider>
      <el-row :gutter="20">
        <el-col :span="5" :offset="1">
          <el-form-item label="权限验证：" prop="permissionValidate">
            <el-switch v-model="form.model.permissionValidate" />
          </el-form-item>
        </el-col>
        <el-col :span="5" :offset="1">
          <el-form-item label="按钮权限：" prop="buttonPermission">
            <el-switch v-model="form.model.buttonPermission" />
          </el-form-item>
        </el-col>
        <el-col :span="5">
          <el-form-item label="审计日志：" prop="auditing">
            <el-switch v-model="form.model.auditing" />
          </el-form-item>
        </el-col>
      </el-row>
    </nm-form-page>
  </nm-container>
</template>
<script>
import { mapState } from 'vuex'
import page from './page'

// 接口
const api = $api.admin.system

export default {
  name: page.name,
  data() {
    return {
      form: {
        title: page.title,
        icon: 'system',
        successMsg: false,
        btnReset: false,
        action: api.updateConfig,
        model: {
          title: '',
          logo: '',
          logoUrl: '',
          home: '',
          userInfoPage: '',
          buttonPermission: false,
          permissionValidate: false,
          auditing: false,
          toolbar: {
            fullscreen: false,
            skin: false,
            logout: false,
            userInfo: false
          },
          customCss: '',
          loginOptions: {
            type: '',
            verifyCode: false
          },
          menuUniqueOpened: false,
          dialogCloseOnClickModal: false,
          copyright: ''
        }
      }
    }
  },
  computed: {
    ...mapState('app/system', ['loginOptions']),
    ...mapState('app/token', ['accessToken']),
    logoUpload() {
      return {
        action: api.uploadLogoUrl(),
        headers: {
          Authorization: 'Bearer ' + this.accessToken
        },
        'show-file-list': false,
        'before-upload': this.beforeLogoUpload,
        'on-success': this.handleLogoSuccess
      }
    }
  },
  methods: {
    query() {
      api.getConfig().then(data => {
        this.form.model = data
        if (!this.form.model.logoUrl) {
          this.form.model.logoUrl = './images/logo.png'
        }
      })
    },
    onSuccess() {
      this._confirm('需要刷新页面才可看到修改后的效果，是否刷新页面？', '修改成功', 'success').then(() => {
        window.location.reload()
      })
    },
    beforeLogoUpload(file) {
      if (file.type !== 'image/jpeg' && file.type !== 'image/png') {
        this._error('上传Logo图片只能是 JPG/PNG 格式!')
        return false
      }
      if (file.size / 1024 > 512) {
        this._error('上传Logo图片大小不能超过 500KB!')
        return false
      }
      return true
    },
    handleLogoSuccess(res) {
      if (res.code === 1) {
        this.form.model.logo = res.data.fullPath
        this.form.model.logoUrl = res.data.url
      }
    }
  },
  created() {
    this.query()
  },
  activated() {
    this.query()
  }
}
</script>
<style lang="scss">
.logo-uploader {
  .el-upload {
    border: 1px dashed #d9d9d9;
    border-radius: 6px;
    cursor: pointer;
    position: relative;
    overflow: hidden;
  }
  .el-upload:hover {
    border-color: #409eff;
  }
  $logo-uploader-width: 75px;
  &-icon {
    font-size: 28px;
    color: #8c939d;
    width: $logo-uploader-width;
    height: $logo-uploader-width;
    line-height: $logo-uploader-width;
    text-align: center;
  }
  &-img {
    width: $logo-uploader-width;
    height: $logo-uploader-width;
    display: block;
  }
}
</style>
