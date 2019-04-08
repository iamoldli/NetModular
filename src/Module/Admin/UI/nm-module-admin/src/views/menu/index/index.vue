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
            <el-form-item label="名称：" prop="name">
              <el-input clearable v-model="list.conditions.name"/>
            </el-form-item>
            <el-form-item label="编码：" prop="code">
              <el-input clearable v-model="list.conditions.code"/>
            </el-form-item>
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
          <details-page :id="previewPage.id" :visible.sync="previewPage.visible"/>
          <!--添加菜单-->
          <add-page :parent="menu" :sort="addPage.sort" :visible.sync="addPage.visible" @success="refresh(true)"/>
          <!--编辑菜单-->
          <edit-page :id="editPage.id" :parent="menu" :visible.sync="editPage.visible" @success="refresh(true)"/>
          <!--按钮绑定-->
          <button-bind-page :visible.sync="buttonBindPage"/>
          <!--权限绑定-->
          <permission-bind-page :visible.sync="permissionBindPage"/>
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
        multiple: true,
        cols,
        action: api.query,
        conditions: {
          parentId: '',
          name: '',
          code: ''
        }
      },
      remove: api.remove,
      addPage: {
        visible: false,
        sort: ''
      },
      editPage: {
        visible: false,
        id: ''
      },
      previewPage: {
        visible: false,
        id: ''
      },
      buttonBindPage: false,
      permissionBindPage: false,
      // 选择的菜单
      menu: { id: '', name: '' },
      buttons: page.buttons
    }
  },
  computed: {
    title () {
      return '菜单列表—' + this.menu.name
    }
  },
  mounted () {
    this.refreshTree()
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
    onTreeSelectChange (data) {
      this.menu.id = data.id
      this.menu.name = data.menu.name
      this.refresh()
    },
    add (total) {
      this.addPage.sort = total
      this.addPage.visible = true
    },
    edit (row) {
      this.editPage.id = row.id
      this.editPage.visible = true
    },
    preview (row) {
      this.previewPage.id = row.id
      this.previewPage.visible = true
    },
    bindButton (row) {
      this.setCurrentMenu(row)
      this.buttonBindPage = true
    },
    bindPermission (row) {
      this.setCurrentMenu(row)
      this.permissionBindPage = true
    }
  }
}
</script>
