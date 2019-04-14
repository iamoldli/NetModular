<template>
  <nm-container>
    <nm-split v-model="split">
      <template v-slot:left>
        <nm-box page header title="菜单树" type="success" icon="menu" :toolbar="null">
          <menu-tree ref="tree" @select-change="onTreeSelectChange"/>
        </nm-box>
      </template>
      <template v-slot:right>
        <nm-list ref="list" :title="title" v-bind="list">
          <!--查询条件-->
          <template v-slot:querybar>
            <el-row :gutter="20">
              <el-col :span="11" :offset="1">
                <el-form-item label="名称：" prop="name">
                  <el-input v-model="list.conditions.name" clearable/>
                </el-form-item>
              </el-col>
              <el-col :span="11">
                <el-form-item label="编码：" prop="code">
                  <el-input v-model="list.conditions.code" clearable/>
                </el-form-item>
              </el-col>
            </el-row>
          </template>

          <!--按钮-->
          <template v-slot:toolbar="{total}">
            <nm-button @click="add(total)" icon="add" text="添加" v-nm-has="buttons.add"/>
          </template>

          <!--名称-->
          <template v-slot:col-name="{row}">
            <nm-button :text="row.name" @click="preview(row)" type="text"/>
          </template>

          <!--类型-->
          <template v-slot:col-typeName="{row}">
            <el-tag type="success" v-if="row.type===0">{{row.typeName}}</el-tag>
            <el-tag type="warning" v-if="row.type===1">{{row.typeName}}</el-tag>
            <el-tag v-if="row.type===2">{{row.typeName}}</el-tag>
          </template>

          <!--图标-->
          <template v-slot:col-icon="{row}">
            <el-tooltip :content="row.icon" effect="dark" placement="right" v-if="row.icon">
              <label>
                <nm-icon :name="row.icon" size="20px"/>
              </label>
            </el-tooltip>
            <label v-else>无</label>
          </template>

          <!--是否显示-->
          <template v-slot:col-show="{row}">{{row.show?'是':'否'}}</template>

          <!--操作列-->
          <template v-slot:col-operation="{row}">
            <el-dropdown trigger="click">
              <span class="el-dropdown-link">
                操作
                <i class="el-icon-arrow-down el-icon--right"></i>
              </span>
              <el-dropdown-menu class="nm-list-operation-dropdown" slot="dropdown">
                <el-dropdown-item>
                  <nm-button @click="edit(row)" icon="edit" text="编辑" type="text" v-nm-has="buttons.edit"/>
                </el-dropdown-item>
                <el-dropdown-item v-if="row.type===1">
                  <nm-button :id="row.id" @click="bindPermission(row)" icon="bind" text="权限" type="text" v-nm-has="buttons.bindPermission"/>
                </el-dropdown-item>
                <el-dropdown-item v-if="row.type===1">
                  <nm-button @click="bindButton(row)" icon="bind" text="按钮" type="text" v-nm-has="buttons.bindButton"/>
                </el-dropdown-item>
                <el-dropdown-item>
                  <nm-button-delete :action="remove" :id="row.id" @success="refresh(true)" v-nm-has="buttons.del"/>
                </el-dropdown-item>
              </el-dropdown-menu>
            </el-dropdown>
          </template>

          <!--菜单详情-->
          <details-page :id="currentMenu.id" :visible.sync="dialog.prev"/>
          <!--添加菜单-->
          <add-page :parent="menu" :sort="total" :visible.sync="dialog.add" @success="refresh(true)"/>
          <!--编辑菜单-->
          <edit-page :parent="menu" :id="currentMenu.id" :visible.sync="dialog.edit" @success="refresh(true)"/>
          <!--按钮绑定-->
          <button-bind-page :menu="currentMenu" :visible.sync="dialog.btnBind"/>
          <!--权限绑定-->
          <permission-bind-page v-bind="currentMenu" :visible.sync="dialog.perBind"/>
        </nm-list>
      </template>
    </nm-split>
  </nm-container>
</template>
<script>
import { mapMutations } from 'vuex'
import page from './page'
import api from '../../../api/menu'
import cols from './cols.js'
import AddPage from '../add'
import DetailsPage from '../details'
import EditPage from '../edit'
import ButtonBindPage from '../../button/index'
import PermissionBindPage from '../premission-bind'
import MenuTree from '../tree'

export default {
  name: page.name,
  components: { MenuTree, AddPage, DetailsPage, EditPage, ButtonBindPage, PermissionBindPage },
  data () {
    return {
      split: 0.2,
      list: {
        cols,
        action: api.query,
        labelWidth: '60px',
        conditions: {
          parentId: '',
          name: '',
          code: ''
        }
      },
      remove: api.remove,
      // 当前数量
      total: 0,
      // 弹出框
      dialog: {
        add: false,
        edit: false,
        // 预览
        prev: false,
        // 按钮绑定
        btnBind: false,
        // 权限绑定
        perBind: false
      },
      // 当前要操作的菜单
      currentMenu: {},
      // 左侧菜单树选择的菜单
      menu: {
        id: '',
        name: '',
        path: ''
      },
      buttons: page.buttons
    }
  },
  computed: {
    title () { return '菜单列表—' + this.menu.name }
  },
  methods: {
    ...mapMutations('module/admin', ['setCurrentMenu']),
    refresh (refreshTree) {
      this.list.conditions.parentId = this.menu.id
      this.$refs.list.refresh()
      // 刷新菜单树
      if (refreshTree) { this.refreshTree() }
    },
    refreshTree () {
      this.$refs.tree.refresh()
    },
    onTreeSelectChange ({ menu, path }) {
      this.menu.id = menu.id
      this.menu.name = menu.name
      this.menu.path = path
      this.refresh()
    },
    add (total) {
      this.total = total
      this.dialog.add = true
    },
    edit (row) {
      this.currentMenu = row
      this.dialog.edit = true
    },
    preview (row) {
      this.currentMenu = row
      this.dialog.prev = true
    },
    bindButton (row) {
      this.currentMenu = row
      this.dialog.btnBind = true
    },
    bindPermission (row) {
      this.currentMenu = row
      this.dialog.perBind = true
    }
  }
}
</script>
