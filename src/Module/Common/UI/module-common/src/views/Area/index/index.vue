<template>
  <nm-container>
    <nm-split v-model="split">
      <template v-slot:left>
        <nm-box page header title="行政区划代码" type="success" icon="menu" :toolbar="null">
          <el-tree
            ref="tree"
            node-key="id"
            highlight-current
            lazy
            :data="tree"
            :load="treeLoad"
            :show-checkbox="false"
            :expand-on-click-node="false"
            :current-node-key="0"
            :default-expanded-keys="[0]"
            :props="{isLeaf:'leaf'}"
            @current-change="onSelectChange"
          ></el-tree>
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
          <template v-slot:querybar-buttons>
            <nm-button-has :options="buttons.add" @click="add" />
          </template>

          <!--操作列-->
          <template v-slot:col-operation="{row}">
            <nm-button-has :options="buttons.edi" @click="edit(row)" />
            <nm-button-delete :options="buttons.del" :action="removeAction" :id="row.id" @success="refresh" />
          </template>
        </nm-list>
      </template>
    </nm-split>
    <!--添加-->
    <add-page :parent-id="parent.id" :full-path="parent.fullPath" :visible.sync="dialog.add" @success="refresh" />
    <!--编辑-->
    <edit-page :id="curr.id" :full-path="parent.fullPath" :visible.sync="dialog.edit" @success="refresh" />
  </nm-container>
</template>
<script>
import api from '../../../api/Area'
import page from './page'
import cols from './cols'
import AddPage from '../components/add'
import EditPage from '../components/edit'
export default {
  name: page.name,
  components: { AddPage, EditPage },
  data() {
    return {
      split: 0.2,
      curr: { id: '' },
      list: {
        title: page.title,
        cols,
        action: api.query,
        model: {
          parentId: 0,
          /** 名称 */
          name: ''
        },
        operationWidth: 130,
        loading: false,
        loadingText: '正在努力爬取区域数据，请稍后...'
      },
      removeAction: api.remove,
      dialog: {
        add: false,
        edit: false
      },
      buttons: page.buttons,
      tree: [],
      parent: {
        id: 0,
        fullPath: ''
      }
    }
  },
  methods: {
    async treeLoad(node, resolve) {
      const id = node.data.id
      if (id !== 0 && !id) {
        resolve([{ id: 0, label: '行政区划代码' }])
      } else {
        const data = await api.queryChildren(id)
        const children = data.map(item => {
          return {
            id: item.id,
            label: item.name,
            leaf: item.level === 3
          }
        })
        resolve(children)
      }
    },
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
    onSelectChange(data, node) {
      this.parent.id = data.id
      this.parent.fullPath = this.getFullPath(node)
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
    }
  }
}
</script>
