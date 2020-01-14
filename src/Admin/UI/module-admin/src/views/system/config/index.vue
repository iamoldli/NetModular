<template>
  <nm-container>
    <nm-tabs>
      <el-tabs v-model="tab">
        <!--基础配置-->
        <el-tab-pane name="base">
          <span slot="label"> <nm-icon name="basic-data"></nm-icon>基础配置 </span>
          <base-config :model="config.base" />
        </el-tab-pane>
        <!--权限配置-->
        <el-tab-pane name="permission">
          <span slot="label"> <nm-icon name="permission"></nm-icon>权限配置 </span>
          <permission-config :model="config.permission" />
        </el-tab-pane>
        <!--登录配置-->
        <el-tab-pane name="login">
          <span slot="label"> <nm-icon name="login"></nm-icon>登录配置 </span>
          <login-config :model="config.login" />
        </el-tab-pane>
        <!--顶部工具栏-->
        <el-tab-pane name="toolbar">
          <span slot="label"> <nm-icon name="tools"></nm-icon>顶部工具栏 </span>
          <toolbar-config :model="config.toolbar" />
        </el-tab-pane>
        <!--组件配置-->
        <el-tab-pane name="component">
          <span slot="label"> <nm-icon name="entity"></nm-icon>组件配置 </span>
          <component-config :model="config.component" />
        </el-tab-pane>
      </el-tabs>
    </nm-tabs>
  </nm-container>
</template>
<script>
import BaseConfig from './components/base'
import PermissionConfig from './components/permission'
import LoginConfig from './components/login'
import ToolbarConfig from './components/toolbar'
import ComponentConfig from './components/component'
const api = $api.admin.system
export default {
  components: { BaseConfig, PermissionConfig, LoginConfig, ToolbarConfig, ComponentConfig },
  data() {
    return {
      tab: 'base',
      config: {
        base: {
          title: '',
          logo: '',
          home: '',
          userInfoPage: '',
          copyright: ''
        },
        permission: {
          validate: true,
          button: false,
          auditing: false
        },
        login: {
          type: null,
          verifyCode: false
        },
        toolbar: {
          fullscreen: true,
          skin: true,
          logout: true,
          userInfo: true
        },
        component: {
          menu: {
            uniqueOpened: false
          },
          dialog: {
            closeOnClickModal: false
          },
          list: {
            serialNumberName: null
          },
          tabnav: {
            showIcon: true
          },
          customCss: null
        }
      }
    }
  },
  methods: {
    onTabClick() {
      this.$nextTick(() => {
        if (this.$refs[this.tab] && this.$refs[this.tab].doLayout) {
          this.$refs[this.tab].doLayout()
        }
      })
    }
  },
  created() {
    api.getConfig().then(data => {
      this.$_.merge(this.config, data)
    })
  }
}
</script>
