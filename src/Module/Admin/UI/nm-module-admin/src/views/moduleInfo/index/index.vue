<template>
  <nm-container>
    <nm-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item label="名称：" prop="name">
          <el-input v-model="list.conditions.name" clearable/>
        </el-form-item>
      </template>

      <!--按钮-->
      <template v-slot:toolbar="{total}">
        <nm-button text="添加" icon="add" @click="add(total)" v-nm-has="buttons.add"/>
      </template>

      <!--是否显示-->
      <template v-slot:col-isShow="{row}">{{row.isShow?'是':'否'}}</template>

      <!--操作列-->
      <template v-slot:col-operation="{row}">
        <nm-button text="编辑" icon="edit" type="text" @click="edit(row)" v-nm-has="buttons.edit"/>
        <nm-button-delete :action="removeAction" :id="row.id" @success="refresh" v-nm-has="buttons.del"/>
      </template>
    </nm-list>

    <!--添加-->
    <add-page :visible.sync="addPage.visible" :sort="addPage.sort" @success="refresh"/>
    <!--编辑-->
    <edit-page :visible.sync="editPage.visible" :id="editPage.id" @success="refresh"/>
  </nm-container>
</template>
<script>
import api from '../../../api/moduleInfo.js'
import page from './page'
import cols from './cols'
import AddPage from '../add'
import EditPage from '../edit'

export default {
  name: page.name,
  components: { AddPage, EditPage },
  data () {
    return {
      list: {
        title: page.title,
        cols,
        action: api.query,
        conditions: {
          name: ''
        }
      },
      removeAction: api.remove,
      addPage: {
        visible: false,
        sort: 0
      },
      editPage: {
        visible: false,
        id: ''
      },
      buttons: page.buttons
    }
  },
  methods: {
    refresh () {
      this.$refs.list.refresh()
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
    }
  }
}
</script>
