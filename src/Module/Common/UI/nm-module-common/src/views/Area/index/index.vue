<template>
  <nm-container>
    <nm-split v-model="split">
      <template v-slot:left>
        <!-- <el-tree ref="tree" class="nm-admin-menu-tree" node-key="id" default-expand-all highlight-current :data="tree" :show-checkbox="false" :expand-on-click-node="false">
          <div class="nm-menu-tree-node" slot-scope="{ node, data }">
            <div class="nm-menu-tree-node-label">
              <nm-icon v-if="showIcon" :name="data.menu.icon"/>
              {{ node.label }}
            </div>
          </div>
        </el-tree>-->
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
          <template v-slot:querybar-buttons>
            <nm-button type="success" :text="buttons.add.text" :icon="buttons.add.icon" @click="add" v-nm-has="buttons.add"/>
            <nm-button type="warning" :text="buttons.crawling.text" :icon="buttons.crawling.icon" @click="crawling" v-nm-has="buttons.crawling"/>
          </template>

          <!--操作列-->
          <template v-slot:col-operation="{row}">
            <nm-button :text="buttons.edit.text" :icon="buttons.edit.icon" type="text" @click="edit(row)" v-nm-has="buttons.edit"/>
            <nm-button-delete :id="row.id" :action="removeAction" @success="refresh" v-nm-has="buttons.del"/>
          </template>
        </nm-list>
      </template>
    </nm-split>
    <!--添加-->
    <add-page :visible.sync="dialog.add" @success="refresh"/>
    <!--编辑-->
    <edit-page :id="curr.id" :visible.sync="dialog.edit" @success="refresh"/>
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
          /** 名称 */
          name: ''
        },
        loading: false,
        loadingText: '正在努力爬取区域数据，请稍后...'
      },
      removeAction: api.remove,
      dialog: {
        add: false,
        edit: false
      },
      buttons: page.buttons
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
    crawling() {
      this._confirm('爬取区域数据会清空现有的数据，您确定要爬取吗？').then(() => {
        this.list.loading = true
        api.crawling().then(() => {
          this.list.loading = false
        })
      })
    }
  }
}
</script>
