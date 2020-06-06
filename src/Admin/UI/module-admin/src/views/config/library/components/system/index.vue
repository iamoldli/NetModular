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
          <nm-file-upload-img v-model="form.model.logo" module="Admin" group="Logo" :access-mode="1" width="75px" height="75px" no-text icon-size="3em" @success="onSuccess" />
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="13" :offset="1">
        <el-form-item label="账户信息页：" prop="userPage">
          <el-input v-model="form.model.userPage" placeholder="请填写前端路由名称，默认使用系统自带的userinfo" />
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
      code: 'System',
      form: {
        model: {
          title: '',
          logo: '',
          copyright: '',
          userPage: ''
        }
      }
    }
  },
  methods: {
    onSuccess(data) {
      const { title, copyright, userPage } = this.form.model
      this.setUIConfig({
        system: {
          title,
          logo: data.url,
          copyright,
          userPage
        }
      })
    }
  }
}
</script>
