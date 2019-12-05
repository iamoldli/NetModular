<template>
  <nm-form-page v-bind="form" @success="onSuccess">
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
            <img v-if="logoUrl" :src="logoUrl" class="logo-uploader-img" />
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
          <el-input v-model="form.model.copyright" placeholder="版权声明：XXXXXX" />
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
        action: api.updateBaseConfig,
        model: this.model
      },
      logoUrl: this.model.logo || './images/logo.png'
    }
  },
  props: {
    model: Object
  },
  computed: {
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
    ...mapMutations('app/system', ['setBaseConfig']),
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
        this.logoUrl = res.data.url
      }
    },
    onSuccess() {
      const { title, home, userInfoPage, copyright } = this.form.model
      this.setBaseConfig({ title, home, userInfoPage, copyright, logo: this.logoUrl })
    }
  },
  watch: {
    model() {
      this.form.model = this.model
      this.logoUrl = this.model.logo || './images/logo.png'
    }
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
