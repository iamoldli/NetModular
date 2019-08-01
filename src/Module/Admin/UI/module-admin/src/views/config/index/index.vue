<template>
  <nm-container>
    <nm-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item label="键或值：" prop="key">
          <el-input v-model="list.model.key" clearable />
        </el-form-item>
      </template>

      <!--按钮-->
      <template v-slot:querybar-buttons>
        <nm-button-has :options="buttons.add" @click="add(total)" />
      </template>

      <!--操作列-->
      <template v-slot:col-operation="{row}">
        <nm-button-has :options="buttons.edit" @click="edit(row)" />
        <nm-button-delete :options="buttons.del" :action="removeAction" :id="row.id" @success="refresh" />
      </template>
    </nm-list>

    <!--添加页-->
    <add-page :visible.sync="addPage.visible" @success="refresh" />
    <!--编辑页-->
    <edit-page :id="editDialog.id" :visible.sync="editDialog.visible" @success="refresh" />
  </nm-container>
</template>
<script>
import api from '../../../api/config'
import page from './page'
import cols from './cols'
import AddPage from '../components/add'
import EditPage from '../components/edit'

export default {
  name: page.name,
  components: { AddPage, EditPage },
  data() {
    return {
      list: {
        title: page.title,
        cols,
        action: api.query,
        model: {
          key: ''
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
