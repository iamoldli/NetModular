<template>
  <nm-container>
    <nm-split v-model="split">
      <template v-slot:left>
        <nm-box page header title="菜单树" type="success" icon="menu" :toolbar="null">
          <menu-tree ref="tree" @select-change="onTreeSelectChange" />
        </nm-box>
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
          <template v-slot:querybar-buttons="{total}">
            <nm-button-has :options="buttons.add" @click="add(total)" />
            <nm-button-has :options="buttons.sort" @click="openSort" />
          </template>

          <!--类型-->
          <template v-slot:col-typeName="{row}">
            <el-tag type="success" v-if="row.type===0">{{row.typeName}}</el-tag>
            <el-tag type="warning" v-if="row.type===1">{{row.typeName}}</el-tag>
            <el-tag v-if="row.type===2">{{row.typeName}}</el-tag>
          </template>

          <!--图标-->
          <template v-slot:col-icon="{row}">
            <el-tooltip :content="row.icon" effect="dark" placement="right" v-if="row.icon">
              <label>
                <nm-icon :name="row.icon" size="20px" />
              </label>
            </el-tooltip>
            <label v-else>无</label>
          </template>

          <!--是否显示-->
          <template v-slot:col-show="{row}">{{row.show?'是':'否'}}</template>

          <!--操作列-->
          <template v-slot:col-operation="{row}">
            <el-dropdown trigger="click">
              <span class="el-dropdown-link">
                操作
                <i class="el-icon-arrow-down el-icon--right"></i>
              </span>
              <el-dropdown-menu class="nm-list-operation-dropdown" slot="dropdown">
                <el-dropdown-item>
                  <nm-button-has :options="buttons.edit" @click="edit(row)" />
                </el-dropdown-item>
                <el-dropdown-item>
                  <nm-button-delete :options="buttons.del" :action="remove" :id="row.id" @success="refresh(true)" />
                </el-dropdown-item>
              </el-dropdown-menu>
            </el-dropdown>
          </template>
        </nm-list>
      </template>
    </nm-split>

    <!--添加菜单-->
    <add-page :parent="menu" :sort="total" :visible.sync="dialog.add" @success="refresh(true)" />
    <!--编辑菜单-->
    <edit-page :parent="menu" :id="currentMenu.id" :visible.sync="dialog.edit" @success="refresh(true)" />
    <!--排序-->
    <nm-drag-sort-dialog v-bind="dragSort" :visible.sync="dialog.sort" @success="refresh(true)" />
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
    title() { return '菜单列表—' + this.menu.name },
    dragSort() {
      return {
        queryAction: this.querySortList,
        updateAction: api.updateSortList
      }
    }
  },
  methods: {
    ...mapMutations('module/admin', ['setCurrentMenu']),
    refresh(refreshTree) {
      this.list.model.parentId = this.menu.id
      this.$refs.list.refresh()
      // 刷新菜单树
      if (refreshTree) { this.refreshTree() }
    },
    refreshTree() {
      this.$refs.tree.refresh()
    },
    onTreeSelectChange({ menu, path }) {
      this.menu.id = menu.id
      this.menu.name = menu.name
      this.menu.path = path
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
    }
  }
}
</script>
