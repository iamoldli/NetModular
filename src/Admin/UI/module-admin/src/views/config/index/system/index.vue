<template>
  <el-tabs class="nm-admin-config-system" v-model="tab" tab-position="left" type="border-card">
    <!--基础配置-->
    <el-tab-pane name="base">
      <span slot="label"> <nm-icon name="basic-data"></nm-icon>基础配置 </span>
      <base-config ref="base" :model="config.base" />
    </el-tab-pane>
    <!--权限配置-->
    <el-tab-pane name="permission">
      <span slot="label"> <nm-icon name="permission"></nm-icon>权限配置 </span>
      <permission-config ref="permission" :model="config.permission" />
    </el-tab-pane>
    <!--登录配置-->
    <el-tab-pane name="login">
      <span slot="label"> <nm-icon name="login"></nm-icon>登录配置 </span>
      <login-config ref="login" :model="config.login" />
    </el-tab-pane>
    <!--顶部工具栏-->
    <el-tab-pane name="toolbar">
      <span slot="label"> <nm-icon name="tools"></nm-icon>工具配置 </span>
      <toolbar-config ref="toolbar" :model="config.toolbar" />
    </el-tab-pane>
    <!--组件配置-->
    <el-tab-pane name="component">
      <span slot="label"> <nm-icon name="entity"></nm-icon>组件配置 </span>
      <component-config ref="component" :model="config.component" />
    </el-tab-pane>
    <!--路径配置-->
    <el-tab-pane name="path">
      <span slot="label"> <nm-icon name="attachment"></nm-icon>路径配置 </span>
      <path-config ref="path" :model="config.path" />
    </el-tab-pane>
  </el-tabs>
</template>
<script>
import BaseConfig from './components/base'
import PermissionConfig from './components/permission'
import LoginConfig from './components/login'
import ToolbarConfig from './components/toolbar'
import ComponentConfig from './components/component'
import PathConfig from './components/path'
const api = $api.admin.system
export default {
  components: { BaseConfig, PermissionConfig, LoginConfig, ToolbarConfig, ComponentConfig, PathConfig },
  data() {
    return {
      tab: 'base',
      config: {
        base: {
          title: '',
          logo: '',
          logoUrl: '',
          home: '',
          userInfoPage: '',
          copyright: ''
        },
        permission: {
          validate: true,
          button: false
        },
        login: {
          type: null,
          verifyCode: false,
          singleAccount: false
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
            closeOnClickModal: false,
            draggable: false
          },
          list: {
            serialNumberName: null
          },
          tabnav: {
            showIcon: true,
            maxOpenCount: 20
          },
          customCss: null
        },
        path: {
          uploadPath: null,
          tempPath: null
        }
      }
    }
  },
  created() {
    api.getConfig().then(data => {
      this.$_.merge(this.config, data)
    })
  }
}
</script>
<style lang="scss">
.nm-admin-config-system {
  height: 100%;

  .el-tabs__header {
    margin-right: 0 !important;
  }

  .el-tabs__item.is-active {
    color: #67c23a !important;
  }
  .el-tabs__content {
    .nm-box {
      border-left: none !important;
    }
  }
}
</style>
