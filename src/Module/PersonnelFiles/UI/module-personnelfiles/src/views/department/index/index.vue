<template>
  <nm-container>
    <nm-split v-model="split">
      <template v-slot:left>
        <nm-box page header title="部门架构" type="success" icon="menu" :toolbar="null">
          <tree ref="tree" v-model="parent.id" @change="onTreeChange" />
        </nm-box>
      </template>
      <template v-slot:right>
        <nm-list ref="list" v-bind="list">
          <!--查询条件-->
          <template v-slot:querybar>
            <el-form-item label="名称：" prop="name">
              <el-input v-model="list.model.name" clearable />
            </el-form-item>
          </template>

          <!--按钮-->
          <template v-slot:querybar-buttons="{total}">
            <nm-button-has :options="buttons.add" @click="add(total)" />
          </template>

          <!--操作列-->
          <template v-slot:col-operation="{row}">
            <nm-button-has :options="buttons.edit" @click="edit(row)" />
            <nm-button-has :options="buttons.position" @click="position(row)" />
            <nm-button-delete :options="buttons.del" :action="removeAction" :id="row.id" @success="onRemoveSuccess(row.id)" />
          </template>
        </nm-list>
      </template>
    </nm-split>

    <!--添加-->
    <add-page v-bind="saveProps" :visible.sync="dialog.add" @success="refreshAll" />
    <!--编辑-->
    <edit-page :id="curr.id" v-bind="saveProps" :visible.sync="dialog.edit" @success="refreshAll" />
    <!--岗位设置-->
    <position-page :department="curr" :visible.sync="dialog.position" />
  </nm-container>
</template>
<script>
import page from './page'
import cols from './cols'
import AddPage from '../components/add'
import EditPage from '../components/edit'
import Tree from '../components/tree'
import PositionPage from '../components/position'

const api = $api.personnelFiles.department

export default {
  name: page.name,
  components: { AddPage, EditPage, PositionPage, Tree },
  data() {
    return {
      split: 0.2,
      curr: { id: '', name: '' },
      total: 0,
      company: {
        id: '',
        name: ''
      },
      parent: {
        id: '',
        fullPath: ''
      },
      list: {
        title: page.title,
        queryOnCreated: false,
        cols,
        action: this.query,
        model: {
          /** 名称 */
          name: '',
          /** 公司编号 */
          companyId: '',
          parentId: ''
        }
      },
      removeAction: api.remove,
      dialog: {
        add: false,
        edit: false,
        position: false
      },
      buttons: page.buttons
    }
  },
  computed: {
    saveProps() {
      return {
        companyId: this.company.id,
        companyName: this.company.name,
        parentId: this.parent.id,
        fullPath: this.parent.fullPath,
        total: this.total
      }
    }
  },
  methods: {
    query() {
      if (!this.company.id) {
        return new Promise(resolve => {
          this._error('请选择公司单位')
          resolve()
        })
      }
      this.list.model.companyId = this.company.id
      return api.query(this.list.model)
    },
    refreshAll() {
      this.$refs.tree.refresh()
      this.refresh()
    },
    refresh() {
      this.$refs.list.refresh()
    },
    add(total) {
      if (!this.company.id) {
        this._warning('请选择公司单位')
        return
      }
      this.dialog.add = true
      this.total = total
    },
    edit(row) {
      this.setCurr(row)
      this.dialog.edit = true
    },
    position(row) {
      this.setCurr(row)
      this.dialog.position = true
    },
    setCurr(row) {
      this.curr.id = row.id
      this.curr.name = this.parent.fullPath + ' / ' + row.name
    },
    onTreeChange(section, company) {
      this.parent.id = section.id
      this.parent.fullPath = section.fullPath
      this.parent.data = section.data
      this.company = company
      this.list.model.parentId = section.id
      this.refresh()
    },
    onRemoveSuccess(id) {
      this.$refs.tree.remove(id)
      this.refresh()
    }
  }
}
</script>
