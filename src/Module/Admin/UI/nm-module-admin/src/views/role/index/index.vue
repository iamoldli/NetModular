<template>
  <nm-container>
    <nm-list ref="list" v-bind="list">
      <!--工具栏-->
      <template v-slot:toolbar>
        <nm-button text="添加" icon="add" @click="addPage.visible=true" v-nm-has="buttons.add"/>
      </template>

      <!--操作列-->
      <template v-slot:col-operation="{row}">
        <el-dropdown trigger="click">
          <span class="el-dropdown-link">
            操作
            <i class="el-icon-arrow-down el-icon--right"></i>
          </span>
          <el-dropdown-menu class="nm-list-operation-dropdown" slot="dropdown">
            <el-dropdown-item>
              <nm-button text="编辑" icon="edit" type="text" @click="edit(row)" v-nm-has="buttons.edit"/>
            </el-dropdown-item>
            <el-dropdown-item>
              <nm-button text="菜单" icon="bind" type="text" @click="bindMenu(row)" v-nm-has="buttons.bindMenu"/>
            </el-dropdown-item>
            <el-dropdown-item>
              <nm-button-delete :action="removeAction" :id="row.id" @success="refresh" v-nm-has="buttons.del"/>
            </el-dropdown-item>
          </el-dropdown-menu>
        </el-dropdown>
      </template>
    </nm-list>

    <!--添加页-->
    <add-page :visible.sync="addPage.visible" @success="refresh"/>
    <!--编辑页-->
    <edit-page :id="editDialog.id" :visible.sync="editDialog.visible" @success="refresh"/>
    <!--绑定菜单-->
    <bind-menu-page :id="bindMenuDialog.id" :visible.sync="bindMenuDialog.visible"/>
  </nm-container>
</template>
<script>
import api from '../../../api/role'
import page from './page'
import cols from './cols'
import AddPage from '../add/'
import EditPage from '../edit/'
import BindMenuPage from '../menu-bind'

export default {
  name: page.name,
  components: { AddPage, EditPage, BindMenuPage },
  data () {
    return {
      list: {
        title: page.title,
        search: {
          advanced: {
            enabled: false
          }
        },
        cols,
        action: api.query,
        conditions: {
          name: ''
        }
      },
      addPage: {
        visible: false
      },
      editDialog: {
        visible: false,
        id: ''
      },
      bindMenuDialog: {
        visible: false,
        id: ''
      },
      removeAction: api.remove,
      buttons: page.buttons
    }
  },
  methods: {
    refresh () {
      this.$refs.list.refresh()
    },
    edit (row) {
      this.editDialog = {
        id: row.id,
        visible: true
      }
    },
    bindMenu (row) {
      this.bindMenuDialog = {
        id: row.id,
        visible: true
      }
    }

  }
}
</script>
