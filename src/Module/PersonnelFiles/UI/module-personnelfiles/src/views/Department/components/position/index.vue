<template>
  <nm-list-dialog v-bind="dialogOptions" @open="onOpen" :visible.sync="visible_">
    <nm-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item label="名称：" prop="name">
          <el-input v-model="list.model.name" clearable />
        </el-form-item>
      </template>

      <!--按钮-->
      <template v-slot:querybar-buttons>
        <nm-button-has :options="buttons.positionAdd" @click="add(total)" />
      </template>

      <!--操作列-->
      <template v-slot:col-operation="{row}">
        <nm-button-has :options="buttons.positionEdit" @click="edit(row)" />
        <nm-button-delete :options="buttons.positionDel" :action="removeAction" :id="row.id" @success="refresh" />
      </template>
    </nm-list>

    <!--添加-->
    <add-page :department="department" :visible.sync="dialog.add" @success="refresh" />
    <!--编辑-->
    <edit-page :id="curr.id" :department="department" :visible.sync="dialog.edit" @success="refresh" />
  </nm-list-dialog>
</template>
<script>
import api from '../../../../api/Position'
import { mixins } from 'nm-lib-skins'
import page from '../../index/page'
import cols from './cols'
import AddPage from '../../../Position/components/add'
import EditPage from '../../../Position/components/edit'
export default {
  components: { AddPage, EditPage },
  mixins: [mixins.dialog],
  data() {
    return {
      curr: { id: '' },
      list: {
        noHeader: true,
        cols,
        action: api.query,
        model: {
          /** 名称 */
          name: '',
          /** 部门编码 */
          departmentId: ''
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
  props: {
    department: {
      type: Object,
      required: true
    }
  },
  computed: {
    dialogOptions() {
      return {
        title: `岗位列表(${this.department.name})`,
        icon: 'list',
        width: '80%',
        height: '80%'
      }
    }
  },
  methods: {
    refresh() {
      this.$nextTick(() => {
        this.$refs.list.refresh()
      })
    },
    add() {
      this.dialog.add = true
    },
    edit(row) {
      this.curr = row
      this.dialog.edit = true
    },
    onOpen() {
      if (this.list.model.departmentId !== this.department.id) {
        this.list.model.departmentId = this.department.id
        this.refresh()
      }
    }
  }
}
</script>
