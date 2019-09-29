<template>
  <nm-list-dialog class="nm-module-code-generator-property" :title=" `属性列表(${parent.name})`" icon="list" width="70%" :visible.sync="visible_" @open="onOpen">
    <nm-list ref="list" v-bind="list">
      <template v-slot:querybar>
        <el-form-item label="名称：" prop="name">
          <el-input v-model="list.model.name" clearable />
        </el-form-item>
      </template>

      <template v-slot:querybar-buttons="{total}">
        <nm-button type="success" text="添加" icon="add" @click="add(total)" />
        <nm-button type="warning" text="排序" icon="sort" @click="openSort" />
      </template>

      <template v-slot:col-name="{row}">
        <el-tooltip v-if="row.isInherit" effect="dark" content="继承自基类实体" placement="top">
          <nm-icon class="icon-inherit" name="star-fill" />
        </el-tooltip>
        <el-tooltip v-if="row.isPrimaryKey" effect="dark" content="主键" placement="top">
          <nm-icon class="icon-primarykey" name="key" />
        </el-tooltip>
        <span>{{row.name}}</span>
      </template>

      <template v-slot:col-nullable="{row}">
        <el-switch :value="row.nullable" @change="updateNullable(row)" :disabled="row.isInherit" />
      </template>

      <template v-slot:col-showInList="{row}">
        <el-switch :value="row.showInList" @change="updateShowInList(row)" :disabled="row.isInherit" />
      </template>

      <template v-slot:col-operation="{row}">
        <nm-button text="编辑" icon="edit" type="text" @click="edit(row)" :disabled="row.isInherit" />
        <nm-button-delete :action="removeAction" :id="row.id" @success="refresh" :disabled="row.isInherit" />
      </template>
    </nm-list>
    <add-page :total="total" :parent="parent" :visible.sync="dialog.add" @success="refresh" />
    <edit-page :id="curr.id" :parent="parent" :visible.sync="dialog.edit" @success="refresh" />
    <nm-drag-sort-dialog v-bind="dragSort" :visible.sync="dialog.sort" @success="refresh" />
  </nm-list-dialog>
</template>
<script>
import { mixins } from 'nm-lib-skins'
import api from '../../../api/property'
import cols from './cols'
import AddPage from '../components/add'
import EditPage from '../components/edit'
export default {
  mixins: [mixins.dialog],
  components: { AddPage, EditPage },
  data() {
    return {
      curr: { id: '' },
      total: 0,
      list: {
        noHeader: true,
        queryOnCreated: false,
        action: api.query,
        operationWidth: 150,
        model: {
          classId: '',
          name: ''
        },
        cols
      },
      dialog: {
        add: false,
        edit: false,
        property: false,
        sort: false
      },
      removeAction: api.remove
    }
  },
  props: {
    parent: {
      type: Object,
      required: true
    }
  },
  computed: {
    dragSort() {
      return {
        queryAction: this.querySortList,
        updateAction: api.updateSortList
      }
    }
  },
  methods: {
    refresh() {
      this.$refs.list.refresh()
    },
    add(total) {
      this.total = total
      this.dialog.add = true
    },
    edit(row) {
      this.curr = row
      this.dialog.edit = true
    },
    updateNullable(row) {
      row.nullable = !row.nullable
      const { id, nullable } = row
      api.updateNullable({ id, nullable })
    },
    updateShowInList(row) {
      row.showInList = !row.showInList
      const { id, showInList } = row
      api.updateShowInList({ id, showInList })
    },
    querySortList() {
      return api.querySortList(this.parent.id)
    },
    openSort() {
      this.dialog.sort = true
    },
    onOpen() {
      this.$nextTick(() => {
        this.$refs.list.reset()
        this.list.model.classId = this.parent.id
        this.$refs.list.query()
      })
    }
  }
}
</script>
<style lang="scss">
.nm-module-code-generator-property {
  .icon-inherit,
  .icon-primarykey {
    font-size: 1.8em;
    cursor: pointer;
  }
  .icon-inherit {
    color: #dd4b39;
  }
  .icon-primarykey {
    color: #f39c12;
  }
}
</style>
