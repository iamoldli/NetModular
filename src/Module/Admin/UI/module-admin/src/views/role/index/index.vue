<template>
  <nm-container>
    <nm-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item label="名称：" prop="name">
          <el-input v-model="list.model.name" clearable />
        </el-form-item>
      </template>

      <!--按钮-->
      <template v-slot:querybar-buttons>
        <nm-button-has :options="buttons.add" @click="addPage.visible=true" />
      </template>

      <template v-slot:col-name="{row}">
        <el-tooltip v-if="row.isSpecified" class="item" effect="dark" content="指定角色" placement="top">
          <div>
            <nm-icon class="nm-text-warning" style="font-size:25px" name="star-fill" />
            <span>{{row.name}}</span>
          </div>
        </el-tooltip>
        <span v-else>{{row.name}}</span>
      </template>

      <!--操作列-->
      <template v-slot:col-operation="{row}">
        <nm-button-has :options="buttons.edit" @click="edit(row)" :disabled="row.isSpecified" />
        <nm-button-has :options="buttons.bindMenu" @click="bindMenu(row)" />
        <nm-button-delete :options="buttons.del" :action="removeAction" :id="row.id" @success="refresh" :disabled="row.isSpecified" />
      </template>
    </nm-list>

    <!--添加页-->
    <add-page :visible.sync="addPage.visible" @success="refresh" />
    <!--编辑页-->
    <edit-page :id="editDialog.id" :visible.sync="editDialog.visible" @success="refresh" />
    <!--绑定菜单-->
    <bind-menu-page :id="bindMenuDialog.id" :visible.sync="bindMenuDialog.visible" />
  </nm-container>
</template>
<script>
import api from '../../../api/role'
import page from './page'
import cols from './cols'
import AddPage from '../components/add'
import EditPage from '../components/edit'
import BindMenuPage from '../components/menu-bind'

export default {
  name: page.name,
  components: { AddPage, EditPage, BindMenuPage },
  data() {
    return {
      list: {
        title: page.title,
        cols,
        action: api.query,
        model: {
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
    refresh() {
      this.$refs.list.refresh()
    },
    edit(row) {
      this.editDialog = {
        id: row.id,
        visible: true
      }
    },
    bindMenu(row) {
      this.bindMenuDialog = {
        id: row.id,
        visible: true
      }
    }

  }
}
</script>
