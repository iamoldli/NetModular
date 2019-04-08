<template>
  <nm-list-dialog v-bind="dialog" @open="onOpen" :visible.sync="visible_">
    <nm-list ref="list" v-bind="list">
      <!--工具栏-->
      <template v-slot:toolbar="{total}">
        <nm-button text="添加" icon="add" type="success" @click="add(total)"/>
      </template>

      <!--操作列-->
      <template v-slot:col-operation="{row}">
        <nm-button text="编辑" icon="edit" type="text" @click="edit(row)"/>
        <nm-button text="权限" icon="bind" type="text" @click="bindPermission(row)"/>
        <nm-button-delete :action="removeAction" :id="row.id" @success="refresh"/>
      </template>
    </nm-list>

    <!--添加-->
    <add-page :visible.sync="addPage.visible" :sort="addPage.sort" @success="refresh"/>
    <!--编辑-->
    <edit-page :visible.sync="editPage.visible" :id="editPage.id" @success="refresh"/>
    <!--绑定权限-->
    <permission-bind-page :visible.sync="permissionBindPage.visible" :button="permissionBindPage.button"/>
  </nm-list-dialog>
</template>
<script>
import { mapState } from 'vuex'
import { mixins } from 'nm-lib-skins'
import api from '../../../api/button'
import cols from './cols.js'
import AddPage from '../add'
import EditPage from '../edit'
import PermissionBindPage from '../permission-bind'
export default {
  mixins: [mixins.dialog],
  components: { AddPage, EditPage, PermissionBindPage },
  data () {
    return {
      list: {
        noHeader: true,
        search: {
          advanced: { enabled: false }
        },
        action: api.query,
        conditions: {
          menuId: '',
          name: ''
        },
        cols
      },
      addPage: {
        visible: false,
        sort: 0
      },
      editPage: {
        visible: false,
        id: ''
      },
      permissionBindPage: {
        visible: false,
        button: {}
      },
      removeAction: api.remove
    }
  },
  computed: {
    ...mapState('module/admin', ['currentMenu']),
    dialog () {
      return {
        title: `按钮管理--${this.currentMenu.name}`,
        icon: 'detail',
        width: '70%',
        height: '80%'
      }
    }
  },
  methods: {
    refresh () {
      if (this.$refs.list) { this.$refs.list.refresh() }
    },
    add (total) {
      this.addPage.sort = total
      this.addPage.visible = true
    },
    edit (row) {
      this.editPage = {
        id: row.id,
        visible: true
      }
    },
    bindPermission (row) {
      this.permissionBindPage.button = row
      this.permissionBindPage.visible = true
    },
    onOpen () {
      const hasChange = this.list.conditions.menuId !== this.currentMenu.id
      this.list.conditions.menuId = this.currentMenu.id
      if (this.list.conditions.menuId && hasChange) {
        this.refresh()
      }
    }
  }
}
</script>
