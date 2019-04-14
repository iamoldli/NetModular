<template>
  <nm-container>
    <nm-form-page ref="form" v-bind="form" @success="onSuccess">
      <nm-divider orientation="left">基础信息</nm-divider>
      <el-row :gutter="20">
        <el-col :span="10" :offset="1">
          <el-form-item label="标题：" prop="title">
            <el-input v-model="form.model.title"/>
          </el-form-item>
          <el-form-item label="首页：" prop="home">
            <el-input v-model="form.model.home"/>
          </el-form-item>
        </el-col>
        <el-col :span="10" :offset="1">
          <el-form-item label="Logo：" prop="logo">
            <el-upload class="logo-uploader" v-bind="logoUpload">
              <img v-if="form.model.logoUrl" :src="form.model.logoUrl" class="logo-uploader-img">
              <i v-else class="el-icon-plus logo-uploader-icon"></i>
            </el-upload>
          </el-form-item>
        </el-col>
      </el-row>
      <nm-divider orientation="left">工具栏</nm-divider>
      <el-row :gutter="20">
        <el-col :span="10" :offset="1">
          <el-form-item label="全屏控制：" prop="toolbar.fullscreen">
            <el-switch v-model="form.model.toolbar.fullscreen"></el-switch>
          </el-form-item>
        </el-col>
        <el-col :span="10" :offset="1">
          <el-form-item label="皮肤设置：" prop="toolbar.skin">
            <el-switch v-model="form.model.toolbar.skin"></el-switch>
          </el-form-item>
        </el-col>
      </el-row>
      <nm-divider orientation="left">开发属性</nm-divider>
      <el-row :gutter="20">
        <el-col :span="10" :offset="1">
          <el-form-item label="按钮权限：" prop="buttonPermission">
            <el-switch v-model="form.model.buttonPermission"></el-switch>
          </el-form-item>
        </el-col>
        <el-col :span="10" :offset="1">
          <el-form-item label="审计日志：" prop="auditing">
            <el-switch v-model="form.model.auditing"></el-switch>
          </el-form-item>
        </el-col>
      </el-row>
    </nm-form-page>
  </nm-container>
</template>
<script>
import api from '../../../api/system'
import page from './page'
import token from '../../../extensions/token.js'
export default {
  name: page.name,
  data () {
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
          buttonPermission: false,
          auditing: false,
          toolbar: {
            fullscreen: false,
            skin: false
          }
        }
      },
      logoUpload: {
        action: api.uploadLogoUrl(),
        headers: {
          Authorization: 'Bearer ' + token.get().accessToken
        },
        'show-file-list': false,
        'before-upload': this.beforeLogoUpload,
        'on-success': this.handleLogoSuccess
      }
    }
  },
  computed: {
    headers () {
      return {
        Authorization: 'Bearer ' + token.get().accessToken
      }
    }
  },
  methods: {
    query () {
      api.getConfig().then(data => {
        this.form.model = data
      })
    },
    onSuccess () {
      this._confirm('需要刷新页面才可看到修改后的效果，是否刷新页面？', '修改成功', 'success').then(() => {
        window.location.reload()
      })
    },
    beforeLogoUpload (file) {
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
    handleLogoSuccess (res, file) {
      if (res.code === 1) {
        this.form.model.logo = res.data.path
        this.form.model.logoUrl = res.data.url
      } else {
        this._error('上传失败')
      }
    }
  },
  created () {
    this.query()
  },
  activated () {
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
