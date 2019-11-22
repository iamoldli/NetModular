<template>
  <nm-container>
    <nm-split v-model="split">
      <template v-slot:left>
        <menu-tree ref="tree" @change="onTreeChange" />
      </template>
      <template v-slot:right>
        <nm-list ref="list" :title="title" v-bind="list">
          <!--查询条件-->
          <template v-slot:querybar>
            <el-form-item label="名称：" prop="name">
              <el-input v-model="list.model.name" clearable />
            </el-form-item>
            <el-form-item label="编码：" prop="code">
              <el-input v-model="list.model.code" clearable />
            </el-form-item>
          </template>

          <!--按钮-->
          <template v-slot:querybar-buttons="{ total }">
            <nm-button v-bind="buttons.add" @click="add(total)" />
            <nm-button v-bind="buttons.sort" @click="openSort" />
          </template>

          <!--类型-->
          <template v-slot:col-typeName="{ row }">
            <el-tag type="success" v-if="row.type === 0">{{ row.typeName }}</el-tag>
            <el-tag type="warning" v-if="row.type === 1">{{ row.typeName }}</el-tag>
            <el-tag v-if="row.type === 2">{{ row.typeName }}</el-tag>
          </template>

          <!--图标-->
          <template v-slot:col-icon="{ row }">
            <el-tooltip :content="row.icon" effect="dark" placement="right" v-if="row.icon">
              <label>
                <nm-icon :name="row.icon" size="20px" />
              </label>
            </el-tooltip>
            <label v-else>无</label>
          </template>

          <!--是否显示-->
          <template v-slot:col-show="{ row }">{{ row.show ? '是' : '否' }}</template>

          <!--操作列-->
          <template v-slot:col-operation="{ row }">
            <el-dropdown trigger="click">
              <span class="el-dropdown-link">
                操作
                <i class="el-icon-arrow-down el-icon--right"></i>
              </span>
              <el-dropdown-menu class="nm-list-operation-dropdown" slot="dropdown">
                <el-dropdown-item>
                  <nm-button v-bind="buttons.edit" @click="edit(row)" />
                </el-dropdown-item>
                <el-dropdown-item>
                  <nm-button-delete v-bind="buttons.del" :action="remove" :id="row.id" @success="onDelete(row.id)" />
                </el-dropdown-item>
              </el-dropdown-menu>
            </el-dropdown>
          </template>
        </nm-list>
      </template>
    </nm-split>

    <!--添加菜单-->
    <add-page :parent="menu" :sort="total" :visible.sync="dialog.add" @success="onAdd" />
    <!--编辑菜单-->
    <edit-page :parent="menu" :id="currentMenu.id" :visible.sync="dialog.edit" @success="onEdit" />
    <!--排序-->
    <nm-drag-sort-dialog v-bind="dragSort" :visible.sync="dialog.sort" @success="onSort" />
  </nm-container>
</template>
<script>
import { mapMutations } from 'vuex'
import page from './page'
import cols from './cols.js'
import AddPage from '../components/add'
import EditPage from '../components/edit'
import MenuTree from '../components/tree'

// 接口
const api = $api.admin.menu

export default {
  name: page.name,
  components: { MenuTree, AddPage, EditPage },
  data() {
    return {
      split: 0.2,
      list: {
        cols,
        action: api.query,
        labelWidth: '60px',
        model: {
          parentId: '',
          name: '',
          code: ''
        }
      },
      remove: api.remove,
      // 当前数量
      total: 0,
      // 弹出框
      dialog: {
        add: false,
        edit: false,
        // 排序
        sort: false
      },
      // 当前要操作的菜单
      currentMenu: {},
      // 左侧菜单树选择的菜单
      menu: {
        id: '',
        name: '',
        path: ''
      },
      buttons: page.buttons
    }
  },
  computed: {
    title() {
      return '菜单列表—' + this.menu.name
    },
    dragSort() {
      return {
        queryAction: this.querySortList,
        updateAction: api.updateSortList
      }
    }
  },
  methods: {
    ...mapMutations('module/admin', ['setCurrentMenu']),
    refresh() {
      this.list.model.parentId = this.menu.id
      this.$refs.list.refresh()
    },
    onTreeChange({ id, label, path }) {
      this.menu.id = id
      this.menu.name = label
      this.menu.path = path.join(' / ')
      this.refresh()
    },
    add(total) {
      this.total = total
      this.dialog.add = true
    },
    edit(row) {
      this.currentMenu = row
      this.dialog.edit = true
    },
    querySortList() {
      return api.querySortList(this.menu.id)
    },
    openSort() {
      this.dialog.sort = true
    },
    onAdd(model) {
      this.$refs.tree.insert(model)
      this.refresh()
    },
    onDelete(id) {
      this.$refs.tree.remove(id)
      this.refresh()
    },
    onEdit(model) {
      this.$refs.tree.update(model)
      this.refresh()
    },
    onSort(data) {
      this.$refs.tree.sort(data)
      this.refresh()
    }
  }
}
</script>
