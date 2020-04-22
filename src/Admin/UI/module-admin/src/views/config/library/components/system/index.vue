<template>
  <nm-form-page v-bind="form" @success="onSuccess">
    <el-row>
      <el-col :span="13" :offset="1">
        <el-form-item label="标题：" prop="title">
          <el-input v-model="form.model.title" />
        </el-form-item>
        <el-form-item label="版权申明：" prop="copyright">
          <el-input v-model="form.model.copyright" placeholder="版权所有：XXXXXX" />
        </el-form-item>
      </el-col>
      <el-col :span="7">
        <el-form-item label="Logo：" prop="logo">
          <el-upload class="logo-uploader" v-bind="logoUpload">
            <img :src="logoUrl || './images/logo.png'" class="logo-uploader-img" />
          </el-upload>
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-page>
</template>
<script>
import mixins from '../../form-mixins'
import { mapState, mapMutations } from 'vuex'
const { uploadLogoUrl, getLogoUrl } = $api.admin.config
export default {
  mixins: [mixins],
  data() {
    return {
      code: 'System',
      form: {
        model: {
          title: '',
          logo: '',
          copyright: ''
        }
      },
      logoUrl: ''
    }
  },
  computed: {
    ...mapState('app/token', ['accessToken']),
    logoUpload() {
      return {
        action: uploadLogoUrl(),
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
      const { title, copyright } = this.form.model
      this.setUIConfig({
        system: {
          title,
          logo: this.logoUrl,
          copyright
        }
      })
    }
  },
  mounted() {
    getLogoUrl().then(url => {
      this.logoUrl = url
    })
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
