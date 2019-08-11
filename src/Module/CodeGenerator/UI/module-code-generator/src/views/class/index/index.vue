<template>
  <nm-list-dialog v-bind="dialogOptions" :visible.sync="visible_" @open="onOpen">
    <nm-list ref="list" v-bind="list">
      <template v-slot:querybar>
        <el-form-item label="名称：" prop="name">
          <el-input v-model="list.model.name" clearable />
        </el-form-item>
      </template>

      <template v-slot:querybar-buttons>
        <nm-button type="success" text="添加" icon="add" @click="add" />
      </template>

      <!--配置属性-->
      <template v-slot:col-name="{row}">
        <el-tooltip class="item" effect="dark" content="点击编辑属性" placement="top">
          <nm-button :text="row.name" type="text" @click="manageProperty(row)" />
        </el-tooltip>
      </template>

      <template v-slot:col-operation="{row}">
        <nm-button text="编辑" icon="edit" type="text" @click="edit(row)" />
        <nm-button text="模型" icon="entity" type="text" @click="openModelManage(row)" />
        <nm-button text="生成代码" icon="download" type="text" @click="buildCode(row)" />
        <!-- <nm-button text="预览代码" icon="display" type="text" @click="codePreview(row)" /> -->
        <nm-button-delete :action="removeAction" :id="row.id" @success="refresh" />
      </template>
    </nm-list>
    <add-page :project="project" :visible.sync="dialog.add" @success="refresh" />
    <edit-page :id="curr.id" :project="project" :visible.sync="dialog.edit" @success="refresh" />
    <property-page :parent="curr" :visible.sync="dialog.property" />
    <model-manage-page :parent="curr" :visible.sync="dialog.model" />
    <code-preview :id="curr.id" :visible.sync="dialog.codePreview" />
  </nm-list-dialog>
</template>
<script>
import { mixins } from 'nm-lib-skins'
import api from '../../../api/class.js'
import cols from './cols.js'
import AddPage from '../components/add'
import EditPage from '../components/edit'
import PropertyPage from '../../property/index'
import ModelManagePage from '../../model-property/index'
import CodePreview from '../components/code-preview'
export default {
  mixins: [mixins.dialog, mixins.loading],
  components: { AddPage, EditPage, PropertyPage, ModelManagePage, CodePreview },
  data() {
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
        model: false,
        codePreview: false
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
    dialogOptions() {
      return {
        title: `类列表(${this.project.name})`,
        icon: 'list',
        width: '80%'
      }
    }
  },
  methods: {
    refresh() {
      this.$refs.list.refresh()
    },
    add() {
      this.dialog.add = true
    },
    edit(row) {
      this.curr = row
      this.dialog.edit = true
    },
    /** 打开模型管理 */
    openModelManage(row) {
      this.curr = row
      this.dialog.model = true
    },
    manageProperty(row) {
      this.curr = row
      this.dialog.property = true
    },
    buildCode(row) {
      let loading = this._loading('正在努力生成代码，请稍后...')
      api.buildCode(row.id).then(() => {
        loading.close()
      }).catch(() => {
        loading.close()
      })
    },
    codePreview(row) {
      this.curr = row
      this.dialog.codePreview = true
    },
    onOpen() {
      this.$nextTick(() => {
        this.$refs.list.reset()
        this.list.model.projectId = this.project.id
        this.$refs.list.query()
      })
    }
  }
}
</script>
