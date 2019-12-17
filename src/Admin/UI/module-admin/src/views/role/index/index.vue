<template>
  <nm-container>
    <nm-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item label="名称：" prop="name">
          <el-input v-model="list.model.name" clearable />
        </el-form-item>
      </template>

      <!--按钮-->
      <template v-slot:querybar-buttons>
        <nm-button v-bind="buttons.add" @click="add" />
      </template>

      <template v-slot:col-name="{ row }">
        <el-tooltip v-if="row.isSpecified" class="item" effect="dark" content="指定角色" placement="top">
          <div>
            <nm-icon class="nm-text-warning" style="font-size:25px" name="star-fill" />
            <span>{{ row.name }}</span>
          </div>
        </el-tooltip>
        <span v-else>{{ row.name }}</span>
      </template>

      <!--操作列-->
      <template v-slot:col-operation="{ row }">
        <nm-button v-bind="buttons.edit" @click="edit(row)" :disabled="row.isSpecified" />
        <nm-button v-bind="buttons.bindMenu" @click="bindMenu(row)" />
        <nm-button-delete v-bind="buttons.del" :action="removeAction" :id="row.id" @success="refresh" :disabled="row.isSpecified" />
      </template>
    </nm-list>

    <!--保存页-->
    <save-page :id="curr.id" :visible.sync="dialog.save" @success="refresh" />
    <!--绑定菜单-->
    <bind-menu-page :id="curr.id" :visible.sync="dialog.bindMenu" />
  </nm-container>
</template>
<script>
import { mixins } from 'netmodular-ui'
import page from './page'
import cols from './cols'
import SavePage from '../components/save'
import BindMenuPage from '../components/menu-bind'

// 接口
const api = $api.admin.role

export default {
  mixins: [mixins.list],
  name: page.name,
  components: { SavePage, BindMenuPage },
  data() {
    return {
      list: {
        title: page.title,
        cols,
        action: api.query,
        model: {
          name: ''
        }
      },
      dialog: {
        bindMenu: false
      },
      removeAction: api.remove,
      buttons: page.buttons
    }
  },
  methods: {
    bindMenu(row) {
      this.curr.id = row.id
      this.dialog.bindMenu = true
    }
  }
}
</script>
