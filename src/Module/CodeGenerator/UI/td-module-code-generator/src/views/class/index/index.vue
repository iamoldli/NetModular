<template>
  <td-list-dialog v-bind="dialogOptions" :visible.sync="visible_" @open="onOpen">
    <td-list ref="list" v-bind="list">
      <template v-slot:querybar>
        <el-form-item label="名称：" prop="name">
          <el-input v-model="list.model.name" clearable/>
        </el-form-item>
      </template>

      <template v-slot:querybar-buttons>
        <td-button type="success" text="添加" icon="add" @click="add"/>
      </template>

      <!--配置属性-->
      <template v-slot:col-name="{row}">
        <td-button :text="row.name" type="text" @click="manageProperty(row)"/>
      </template>

      <template v-slot:col-operation="{row}">
        <td-button text="编辑" icon="edit" type="text" @click="edit(row)"/>
        <td-button-delete :action="removeAction" :id="row.id" @success="refresh"/>
        <td-button text="模型" icon="entity" type="text" @click="openModelManage(row)"/>
      </template>
    </td-list>
    <add-page :project="project" :visible.sync="dialog.add" @success="refresh"/>
    <edit-page :id="curr.id" :project="project" :visible.sync="dialog.edit" @success="refresh"/>
    <property-page :parent="curr" :visible.sync="dialog.property"/>
    <model-manage-page :parent="curr" :visible.sync="dialog.model"/>
  </td-list-dialog>
</template>
<script>
import { mixins } from 'td-lib-skins'
import api from '../../../api/class.js'
import cols from './cols.js'
import AddPage from '../components/add'
import EditPage from '../components/edit'
import PropertyPage from '../../property/index'
import ModelManagePage from '../../model-property/index'
export default {
  mixins: [mixins.dialog],
  components: { AddPage, EditPage, PropertyPage, ModelManagePage },
  data () {
    return {
      curr: { id: '' },
      list: {
        noHeader: true,
        queryOnCreated: false,
        action: api.query,
        model: {
          projectId: '',
          name: ''
        },
        cols
      },
      dialog: {
        add: false,
        edit: false,
        property: false,
        model: false
      },
      removeAction: api.remove
    }
  },
  props: {
    project: {
      type: Object,
      required: true
    }
  },
  computed: {
    dialogOptions () {
      return {
        title: `类列表(${this.project.name})`,
        icon: 'list',
        width: '80%'
      }
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
    /** 打开模型管理 */
    openModelManage (row) {
      this.curr = row
      this.dialog.model = true
    },
    manageProperty (row) {
      console.log(1)
      this.curr = row
      this.dialog.property = true
    },
    onOpen () {
      this.$nextTick(() => {
        this.$refs.list.reset()
        this.list.model.projectId = this.project.id
        this.$refs.list.query()
      })
    }
  }
}
</script>
