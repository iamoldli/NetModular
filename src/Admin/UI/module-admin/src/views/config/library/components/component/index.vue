<template>
  <nm-form-page v-bind="form" @success="onSuccess">
    <el-divider content-position="left">登录页</el-divider>
    <el-row>
      <el-col :span="10" :offset="1">
        <el-form-item label="页类型：" prop="pageType">
          <el-select v-model="form.model.login.pageType">
            <el-option v-for="item in pageTypeOptions" :key="item" :label="item" :value="item"></el-option>
          </el-select>
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="验证码：" prop="verifyCode">
          <el-switch v-model="form.model.login.verifyCode" />
        </el-form-item>
      </el-col>
    </el-row>
    <el-divider content-position="left">工具栏</el-divider>
    <el-row>
      <el-col :span="5" :offset="1">
        <el-form-item label="全屏控制：" prop="fullscreen">
          <el-switch v-model="form.model.toolbar.fullscreen" />
        </el-form-item>
      </el-col>
      <el-col :span="5">
        <el-form-item label="皮肤设置：" prop="skin">
          <el-switch v-model="form.model.toolbar.skin" />
        </el-form-item>
      </el-col>
      <el-col :span="5">
        <el-form-item label="退出按钮：" prop="logout">
          <el-switch v-model="form.model.toolbar.logout" />
        </el-form-item>
      </el-col>
      <el-col :span="5">
        <el-form-item label="用户信息：" prop="userInfo">
          <el-switch v-model="form.model.toolbar.userInfo" />
        </el-form-item>
      </el-col>
    </el-row>
    <el-divider content-position="left">菜单</el-divider>
    <el-row>
      <el-col :span="10" :offset="1">
        <el-form-item label="保持一个子菜单的展开：" prop="menu.uniqueOpened">
          <el-switch v-model="form.model.menu.uniqueOpened" />
        </el-form-item>
      </el-col>
    </el-row>
    <el-divider content-position="left">对话框</el-divider>
    <el-row>
      <el-col :span="10" :offset="1">
        <el-form-item label="可点击模态框关闭：" prop="dialog.closeOnClickModal">
          <el-switch v-model="form.model.dialog.closeOnClickModal" />
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="默认可拖拽：" prop="dialog.draggable">
          <el-switch v-model="form.model.dialog.draggable" />
        </el-form-item>
      </el-col>
    </el-row>
    <el-divider content-position="left">列表页</el-divider>
    <el-row>
      <el-col :span="10" :offset="1">
        <el-form-item label="序号表头名称：" prop="list.serialNumberName">
          <el-input v-model="form.model.list.serialNumberName" placeholder="默认 #" />
        </el-form-item>
      </el-col>
    </el-row>
    <el-divider content-position="left">标签导航</el-divider>
    <el-row>
      <el-col :span="10" :offset="1">
        <el-form-item label="启用：" prop="tabnav.enabled">
          <el-switch v-model="form.model.tabnav.enabled" />
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="显示图标：" prop="tabnav.showIcon">
          <el-switch v-model="form.model.tabnav.showIcon" />
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="10" :offset="1">
        <el-form-item label="显示首页" prop="tabnav.showHome">
          <el-switch v-model="form.model.tabnav.showHome" />
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="首页地址：" prop="tabnav.homeUrl">
          <el-input v-model="form.model.tabnav.homeUrl" placeholder="路由的完整地址" />
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="10" :offset="1">
        <el-form-item label="最大页面数量：" prop="tabnav.maxOpenCount">
          <el-input v-model.number="form.model.tabnav.maxOpenCount" placeholder="默认 20" />
        </el-form-item>
      </el-col>
    </el-row>
    <el-divider content-position="left">自定义CSS</el-divider>
    <el-row>
      <el-col :span="22" :offset="1">
        <el-form-item labelWidth="0" prop="customCss">
          <el-input type="textarea" :rows="5" placeholder="如果需要重新某个组件的样式，可以在此处添加覆盖的CSS" v-model="form.model.customCss"></el-input>
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-page>
</template>
<script>
import { mapState } from 'vuex'
import mixins from '../../form-mixins'
export default {
  mixins: [mixins],
  data() {
    return {
      code: 'Component',
      form: {
        labelWidth: '200px',
        model: {
          login: { pageType: null, verifyCode: false },
          menu: { uniqueOpened: true },
          dialog: { closeOnClickModal: false, draggable: false },
          list: { serialNumberName: null },
          tabnav: { enabled: true, showHome: true, homeUrl: null, showIcon: true, maxOpenCount: 20 },
          toolbar: { fullscreen: true, skin: true, logout: true, userInfo: true },
          customCss: null
        }
      }
    }
  },
  computed: {
    ...mapState('app/config', { pageTypeOptions: s => s.component.login.pageTypeOptions })
  },
  methods: {
    onSuccess() {
      this.setUIConfig({
        component: this.form.model
      })
    }
  }
}
</script>
