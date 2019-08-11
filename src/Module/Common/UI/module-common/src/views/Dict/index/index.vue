<template>
  <nm-container>
    <nm-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item label="父节点：" prop="parentId">
          <el-input v-model="list.model.parentId" clearable/>
        </el-form-item>
        <el-form-item label="名称：" prop="name">
          <el-input v-model="list.model.name" clearable/>
        </el-form-item>
      </template>

      <!--按钮-->
      <template v-slot:querybar-buttons>
        <nm-button type="success" :text="buttons.add.text" :icon="buttons.add.icon" @click="add" v-nm-has="buttons.add"/>
      </template>

      <!--操作列-->
      <template v-slot:col-operation="{row}">
        <nm-button :text="buttons.edit.text" :icon="buttons.edit.icon" type="text" @click="edit(row)" v-nm-has="buttons.edit"/>
        <nm-button-delete :id="row.id" :action="removeAction" @success="refresh" v-nm-has="buttons.del"/>
      </template>
    </nm-list>

    <!--添加-->
    <add-page :visible.sync="dialog.add" @success="refresh"/>
    <!--编辑-->
    <edit-page :id="curr.id" :visible.sync="dialog.edit" @success="refresh"/>
  </nm-container>
</template>
<script>
import api from '../../../api/Dict'
import page from './page'
import cols from './cols'
import AddPage from '../components/add'
import EditPage from '../components/edit'

export default {
  name: page.name,
  components: { AddPage, EditPage },
  data () {
    return {
      curr: { id: '' },
      list: {
        title: page.title,
        cols,
        action: api.query,
        model: {
          /** 父节点 */
          parentId: '',
          /** 名称 */
          name: ''
        }
      },
      removeAction: api.remove,
      dialog: {
        add: false,
        edit: false
      },
      buttons: page.buttons
    }
  },
  methods: {
    refresh () {
      this.$refs.list.refresh()
    },
    add () {
      this.dialog.add = true
    },
    edit (row) {
      this.curr = row
      this.dialog.edit = true
    }
  }
}
</script>
