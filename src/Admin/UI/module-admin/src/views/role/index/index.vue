<template>
  <nm-container>
    <nm-split v-model="split">
      <template v-slot:left>
        <role-tree ref="tree" @change="onTreeChange" />
      </template>
      <template v-slot:right>
        <nm-tabs>
          <el-tabs type="border-card">
            <el-tab-pane v-if="_hasButton(buttons.bindMenus)">
              <span slot="label"><nm-icon name="menu" /> 菜单绑定</span>
              <menu-bind :id="roleId" />
            </el-tab-pane>
            <el-tab-pane v-if="_hasButton(buttons.bindPages)" lazy>
              <span slot="label"><nm-icon name="web" /> 页面授权</span>
              <page-bind :id="roleId" />
            </el-tab-pane>
            <el-tab-pane v-if="_hasButton(buttons.bindPlatform)" lazy>
              <span slot="label"><nm-icon name="android" /> 平台授权</span>
              <platform-bind :id="roleId" />
            </el-tab-pane>
          </el-tabs>
        </nm-tabs>
      </template>
    </nm-split>
  </nm-container>
</template>
<script>
import { mixins } from 'netmodular-ui'
import page from './page'
import RoleTree from '../components/tree'
import MenuBind from '../components/menu-bind'
import PageBind from '../components/page-bind'
import PlatformBind from '../components/platform-bind'
export default {
  name: page.name,
  mixins: [mixins.button],
  components: { RoleTree, MenuBind, PageBind, PlatformBind },
  data() {
    return {
      split: 0.2,
      roleId: '',
      buttons: page.buttons
    }
  },
  methods: {
    onTreeChange(data) {
      this.roleId = data.id
    }
  }
}
</script>
<style lang="scss">
.nm-admin-role-menus-tree {
  &-node {
    position: relative;
    padding-left: 300px;
    height: 26px;
    line-height: 26px;

    &-label {
      position: absolute;
      left: 0;
    }
  }
}
</style>
