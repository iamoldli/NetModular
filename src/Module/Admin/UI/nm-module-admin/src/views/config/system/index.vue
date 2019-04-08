<template>
  <nm-container>
    <nm-form-page ref="form" v-bind="form" @success="onSuccess">
      <nm-divider orientation="left">基础信息</nm-divider>
      <el-row :gutter="20">
        <el-col :span="10" :offset="1">
          <el-form-item label="标题" prop="title">
            <el-input v-model="form.model.title"/>
          </el-form-item>
        </el-col>
        <el-col :span="10" :offset="1">
          <el-form-item label="Logo" prop="logo">
            <el-input v-model="form.model.logo"/>
          </el-form-item>
        </el-col>
      </el-row>
      <el-row :gutter="20">
        <el-col :span="10" :offset="1">
          <el-form-item label="首页" prop="home">
            <el-input v-model="form.model.home"/>
          </el-form-item>
        </el-col>
      </el-row>
      <nm-divider orientation="left">工具栏</nm-divider>
      <el-row :gutter="20">
        <el-col :span="10" :offset="1">
          <el-form-item label="全屏控制" prop="toolbar.fullscreen">
            <el-switch v-model="form.model.toolbar.fullscreen"></el-switch>
          </el-form-item>
        </el-col>
        <el-col :span="10" :offset="1">
          <el-form-item label="皮肤设置" prop="toolbar.skin">
            <el-switch v-model="form.model.toolbar.skin"></el-switch>
          </el-form-item>
        </el-col>
      </el-row>
      <nm-divider orientation="left">开发属性</nm-divider>
      <el-row :gutter="20">
        <el-col :span="10" :offset="1">
          <el-form-item label="按钮权限" prop="buttonPermission">
            <el-switch v-model="form.model.buttonPermission"></el-switch>
          </el-form-item>
        </el-col>
        <el-col :span="10" :offset="1">
          <el-form-item label="审计日志" prop="auditing">
            <el-switch v-model="form.model.auditing"></el-switch>
          </el-form-item>
        </el-col>
      </el-row>
    </nm-form-page>
  </nm-container>
</template>
<script>
import api from '../../../api/config'
import page from './page'

export default {
  name: page.name,
  data () {
    return {
      form: {
        title: page.title,
        icon: 'system',
        successMsg: false,
        btnReset: false,
        action: api.updateSystemConfig,
        model: {
          title: '',
          logo: '',
          home: '',
          buttonPermission: false,
          auditing: false,
          toolbar: {
            fullscreen: false,
            skin: false
          }
        },
        rules: {

        }
      }
    }
  },
  methods: {
    query () {
      api.getSystemConfig().then(data => {
        this.form.model = data
      })
    },
    onSuccess () {
      this._confirm('需要刷新页面才可看到修改后的效果，是否刷新页面？', '修改成功', 'success').then(() => {
        window.location.reload()
      })
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
