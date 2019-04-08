<template>
  <nm-container>
    <nm-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item label="模块：" prop="moduleCode">
          <module-info-select v-model="list.conditions.moduleCode" show-refresh/>
        </el-form-item>
        <el-form-item label="名称：" prop="name">
          <el-input v-model="list.conditions.name" clearable/>
        </el-form-item>
        <el-form-item label="控制器：" prop="controller">
          <el-input v-model="list.conditions.controller" clearable/>
        </el-form-item>
        <el-form-item label="方法：" prop="action">
          <el-input v-model="list.conditions.action" clearable/>
        </el-form-item>
      </template>

      <!--工具栏-->
      <template v-slot:toolbar>
        <nm-button text="添加" icon="add" @click="addPage.visible=true" v-nm-has="buttons.add"/>
      </template>

      <template v-slot:col-moduleName="{row}">
        <span>{{`${row.moduleName}(${row.moduleCode})`}}</span>
      </template>

      <!--操作列-->
      <template v-slot:col-operation="{row}">
        <nm-button text="编辑" icon="edit" type="text" @click="edit(row)" v-nm-has="buttons.edit"/>
        <nm-button-delete :action="removeAction" :id="row.id" @success="refresh" v-nm-has="buttons.del"/>
      </template>
    </nm-list>

    <!--添加页-->
    <add-page :visible.sync="addPage.visible" @success="refresh"/>
    <!--编辑页-->
    <edit-page :id="editPage.id" :visible.sync="editPage.visible" @success="refresh"/>
  </nm-container>
</template>
<script>
import api from '../../../api/permission'
import page from './page'
import cols from './cols'
import ModuleInfoSelect from '../../moduleInfo/select/'
import AddPage from '../add/'
import EditPage from '../edit/'

export default {
  name: page.name,
  components: { ModuleInfoSelect, AddPage, EditPage },
  data () {
    return {
      list: {
        title: page.title,
        cols,
        action: api.query,
        search: {
          width: '210px'
        },
        conditions: {
          moduleCode: '',
          name: '',
          controller: '',
          action: ''
        }
      },
      addPage: {
        visible: false
      },
      editPage: {
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
      this.editPage = {
        id: row.id,
        visible: true
      }
    }
  }
}
</script>
