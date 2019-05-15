<template>
  <td-list ref="list" v-bind="list">
    <template v-slot:querybar-buttons="{total}">
      <td-button type="success" text="添加" icon="add" @click="add(total)"/>
      <td-button type="warning" text="排序" icon="sort" @click="openSort"/>
      <td-button type="primary" text="从实体导入" icon="import" @click="openImport(total)"/>
    </template>

    <template v-slot:col-name="{row}">
      <el-tooltip v-if="row.isInherit" effect="dark" content="继承自基类实体" placement="top">
        <td-icon class="icon-inherit" name="star-fill"/>
      </el-tooltip>
      <el-tooltip v-if="row.isPrimaryKey" effect="dark" content="主键" placement="top">
        <td-icon class="icon-primarykey" name="key"/>
      </el-tooltip>
      <span>{{row.name}}</span>
    </template>

    <template v-slot:col-nullable="{row}">
      <el-switch :value="row.nullable" @change="updateNullable(row)"/>
    </template>

    <template v-slot:col-operation="{row}">
      <td-button text="编辑" icon="edit" type="text" @click="edit(row)" :disabled="row.isInherit"/>
      <td-button-delete :action="removeAction" :id="row.id" @success="refresh" :disabled="row.isInherit"/>
    </template>

    <add-page :total="total" :parent="parent" :model-type="modelType" :visible.sync="dialog.add" @success="refresh"/>
    <edit-page :id="curr.id" :parent="parent" :visible.sync="dialog.edit" @success="refresh"/>
    <td-drag-sort-dialog v-bind="dragSort" :visible.sync="dialog.sort" @success="refresh"/>
    <import-page :parent="parent" :model-type="modelType" :total="total" :visible.sync="dialog.import" @success="refresh"/>
  </td-list>
</template>
<script>
import api from '../../../../api/model-property'
import cols from './cols'
import AddPage from '../add'
import EditPage from '../edit'
import ImportPage from '../import'
export default {
  components: { AddPage, EditPage, ImportPage },
  data () {
    return {
      curr: { id: '' },
      total: 0,
      list: {
        noHeader: true,
        queryOnCreated: false,
        noSearch: true,
        action: api.query,
        operationWidth: 150,
        model: {
          classId: this.parent.id,
          modelType: this.modelType
        },
        cols
      },
      dialog: {
        add: false,
        edit: false,
        property: false,
        sort: false,
        import: false
      },
      removeAction: api.remove
    }
  },
  props: {
    parent: {
      type: Object,
      required: true
    },
    modelType: {
      type: Number,
      required: true
    }
  },
  computed: {
    dragSort () {
      return {
        queryAction: this.querySortList,
        updateAction: api.updateSortList
      }
    }
  },
  methods: {
    refresh () {
      this.$refs.list.refresh()
    },
    reset () {
      this.$refs.list.reset()
    },
    add (total) {
      this.total = total
      this.dialog.add = true
    },
    edit (row) {
      this.curr = row
      this.dialog.edit = true
    },
    updateNullable (row) {
      row.nullable = !row.nullable
      const { id, nullable } = row
      api.updateNullable({ id, nullable })
    },
    querySortList () {
      return api.querySortList({ classId: this.parent.id, modelType: this.modelType })
    },
    openSort () {
      this.dialog.sort = true
    },
    openImport (total) {
      this.total = total
      this.dialog.import = true
    }
  },
  watch: {
    parent (val) {
      this.list.model.classId = val.id
      this.refresh()
    },
    modelType (val) {
      this.list.model.modelType = val
      this.refresh()
    }
  }
}
</script>
