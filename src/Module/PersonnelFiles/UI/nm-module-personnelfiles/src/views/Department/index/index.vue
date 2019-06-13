<template>
  <nm-container>
    <nm-split v-model="split">
      <template v-slot:left>
        <nm-box page header title="部门架构" type="success" icon="menu" :toolbar="null">
          <el-form class="nm-pf-dt-company-select" label-width="70px">
            <el-form-item label="公司单位">
              <company-select v-model="list.model.companyId" checked-first @change="onCompanyChange"/>
            </el-form-item>
          </el-form>
          <el-tree ref="tree" v-bind="tree" :current-node-key="parent.id" @current-change="onSelectChange"/>
        </nm-box>
      </template>
      <template v-slot:right>
        <nm-list ref="list" v-bind="list">
          <!--查询条件-->
          <template v-slot:querybar>
            <el-form-item label="名称：" prop="name">
              <el-input v-model="list.model.name" clearable/>
            </el-form-item>
          </template>

          <!--按钮-->
          <template v-slot:querybar-buttons="{total}">
            <nm-button type="success" :text="buttons.add.text" :icon="buttons.add.icon" @click="add(total)" v-nm-has="buttons.add"/>
          </template>

          <!--操作列-->
          <template v-slot:col-operation="{row}">
            <nm-button :text="buttons.edit.text" :icon="buttons.edit.icon" type="text" @click="edit(row)" v-nm-has="buttons.edit"/>
            <nm-button-delete :id="row.id" :action="removeAction" @success="onRemoveSuccess(row.id)" v-nm-has="buttons.del"/>
          </template>
        </nm-list>
      </template>
    </nm-split>

    <!--添加-->
    <add-page v-bind="saveProps" :visible.sync="dialog.add" @success="refreshAll"/>
    <!--编辑-->
    <edit-page :id="curr.id" v-bind="saveProps" :visible.sync="dialog.edit" @success="refreshAll"/>
  </nm-container>
</template>
<script>
import api from '../../../api/Department'
import page from './page'
import cols from './cols'
import AddPage from '../components/add'
import EditPage from '../components/edit'
import CompanySelect from '../../Company/components/select'
export default {
  name: page.name,
  components: { AddPage, EditPage, CompanySelect },
  data() {
    return {
      split: 0.2,
      companyName: '',
      curr: { id: '' },
      total: 0,
      tree: {
        data: [],
        nodeKey: 'id',
        'highlight-current': true,
        'show-checkbox': false,
        'expand-on-click-node': false,
        'default-expand-all': true
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
          companyId: ''
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
  computed: {
    saveProps() {
      return {
        companyId: this.list.model.companyId,
        companyName: this.companyName,
        parentId: this.parent.id,
        fullPath: this.parent.fullPath,
        total: this.total
      }
    }
  },
  methods: {
    query(model) {
      if (!this.list.model.companyId) {
        return new Promise(resolve => {
          this._error('请选择公司单位')
          resolve()
        })
      }

      return api.query(model)
    },
    refreshAll() {
      this.refreshTree()
      this.refresh()
    },
    refresh() {
      this.$refs.list.refresh()
    },
    refreshTree() {
      if (!this.list.model.companyId) {
        this._warning('请选择公司单位')
        return
      }
      let root = { id: '', label: this.companyName, children: [] }
      api.getTree(this.list.model.companyId).then(data => {
        data.map(item => {
          root.children.push(this.model2Tree(item))
        })

        this.tree.data = [root]

        this.$nextTick(() => {
          this.$refs.tree.setCurrentKey(this.parent.id)
        })
      })
    },
    model2Tree(model) {
      let node = {
        id: model.id,
        label: model.name,
        sort: model.sort,
        children: []
      }
      if (model.children) {
        model.children.map(item => [
          node.children.push(this.model2Tree(item))
        ])
      }
      return node
    },
    add(total) {
      if (!this.list.model.companyId) {
        this._warning('请选择公司单位')
        return
      }
      this.dialog.add = true
      this.total = total
    },
    edit(row) {
      this.curr = row
      this.dialog.edit = true
    },
    onCompanyChange(val, selection) {
      this.parent.id = ''
      this.parent.fullPath = ''
      this.companyName = selection.label
      this.refreshTree()
      this.refresh()
    },
    onSelectChange(data, node) {
      this.parent.id = data.id
      this.parent.fullPath = this.getFullPath(node)
      this.parent.data = data.data
      this.list.model.parentId = data.id
      this.refresh()
    },
    /**
     * 获取节点的完整路径
     */
    getFullPath(node) {
      if (node.parent === null || node.data.id === 0) { return '' }
      const parentPath = this.getFullPath(node.parent)
      if (parentPath === '') { return node.data.label } else { return parentPath + ' / ' + node.data.label }
    },
    onRemoveSuccess(id) {
      this.$refs.tree.remove(id)
      this.refresh()
    }
  }
}
</script>
<style lang="scss">
.nm-pf-dt-company-select {
  margin: -5px -10px 10px -10px;
  padding: 0 5px;
  border-bottom: 1px solid #ebeef5;
  .el-form-item {
    margin-bottom: 6px;
  }
}
</style>
