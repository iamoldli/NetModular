<template>
  <td-container>
    <td-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item label="名称：" prop="name">
          <el-input v-model="list.model.name" clearable/>
        </el-form-item>
        <el-form-item label="编码：" prop="code">
          <el-input v-model="list.model.code" clearable/>
        </el-form-item>
      </template>

      <!--按钮-->
      <template v-slot:querybar-buttons>
        <td-button type="success" text="添加" icon="add" @click="add" v-td-has="buttons.add"/>
      </template>

      <!--配置枚举项-->
      <template v-slot:col-name="{row}">
        <td-button :text="row.name" type="text" @click="manageItem(row)"/>
      </template>

      <!--是否显示-->
      <template v-slot:col-isShow="{row}">{{row.isShow?'是':'否'}}</template>

      <!--操作列-->
      <template v-slot:col-operation="{row}">
        <td-button text="编辑" icon="edit" type="text" @click="edit(row)" v-td-has="buttons.edit"/>
        <td-button-delete :action="removeAction" :id="row.id" @success="refresh" v-td-has="buttons.del"/>
      </template>
    </td-list>

    <!--添加-->
    <add-page :visible.sync="dialog.add" @success="refresh"/>
    <!--编辑-->
    <edit-page :id="curr.id" :visible.sync="dialog.edit" @success="refresh"/>
    <!--项管理-->
    <item-page :parent="curr" :visible.sync="dialog.item"/>
  </td-container>
</template>
<script>
import api from '../../../api/enum'
import page from './page'
import cols from './cols'
import AddPage from '../components/add'
import EditPage from '../components/edit'
import ItemPage from '../../enum-item/index'

export default {
  name: page.name,
  components: { AddPage, EditPage, ItemPage },
  data () {
    return {
      curr: { id: '' },
      list: {
        title: page.title,
        cols,
        action: api.query,
        model: {
          name: ''
        }
      },
      removeAction: api.remove,
      dialog: {
        add: false,
        edit: false,
        item: false
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
    },
    manageItem (row) {
      this.curr = row
      this.dialog.item = true
    }
  }
}
</script>
