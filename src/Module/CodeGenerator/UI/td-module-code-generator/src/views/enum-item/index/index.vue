<template>
  <td-list-dialog :title=" `枚举项列表(${parent.name})`" icon="list" height="80%" :visible.sync="visible_" @open="onOpen">
    <td-list ref="list" v-bind="list">
      <template v-slot:querybar>
        <el-form-item label="名称：" prop="name">
          <el-input v-model="list.model.name" clearable/>
        </el-form-item>
      </template>

      <template v-slot:querybar-buttons>
        <td-button type="success" text="添加" icon="add" @click="add"/>
        <td-button type="warning" text="排序" icon="sort" @click="openSort"/>
      </template>

      <template v-slot:col-operation="{row}">
        <td-button text="编辑" icon="edit" type="text" @click="edit(row)"/>
        <td-button-delete :action="removeAction" :id="row.id" @success="refresh"/>
      </template>
    </td-list>
    <add-page :parent="parent" :visible.sync="dialog.add" @success="refresh"/>
    <edit-page :id="curr.id" :parent="parent" :visible.sync="dialog.edit" @success="refresh"/>
    <td-drag-sort-dialog v-bind="dragSort" :visible.sync="dialog.sort" @success="refresh"/>
  </td-list-dialog>
</template>
<script>
import { mixins } from 'td-lib-skins'
import api from '../../../api/enum-item'
import cols from './cols.js'
import AddPage from '../components/add'
import EditPage from '../components/edit'
export default {
  mixins: [mixins.dialog],
  components: { AddPage, EditPage },
  data () {
    return {
      curr: { id: '' },
      list: {
        noHeader: true,
        queryOnCreated: false,
        action: api.query,
        model: {
          enumId: '',
          name: ''
        },
        cols
      },
      dialog: {
        add: false,
        edit: false,
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
    add () {
      this.dialog.add = true
    },
    edit (row) {
      this.curr = row
      this.dialog.edit = true
    },
    openSort () {
      this.dialog.sort = true
    },
    querySortList () {
      return api.querySortList(this.parent.id)
    },
    onOpen () {
      this.$nextTick(() => {
        this.$refs.list.reset()
        this.list.model.enumId = this.parent.id
        this.$refs.list.query()
      })
    }
  }
}
</script>
