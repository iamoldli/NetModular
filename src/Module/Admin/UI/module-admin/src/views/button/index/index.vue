<template>
  <nm-list-dialog v-bind="dialog_" @open="onOpen" :visible.sync="visible_">
    <nm-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item label="名称：" prop="name">
          <el-input v-model="list.model.name" clearable/>
        </el-form-item>
      </template>

      <!--按钮-->
      <template v-slot:querybar-buttons>
        <nm-button type="success" text="同步" icon="refresh" @click="sync"/>
      </template>

      <!--操作列-->
      <template v-slot:col-operation="{row}">
        <nm-button text="权限" icon="bind" type="text" @click="bindPermission(row)"/>
        <nm-button-delete :action="removeAction" :id="row.id" @success="refresh"/>
      </template>
    </nm-list>

    <!--绑定权限-->
    <permission-bind-page v-bind="curr" :visible.sync="dialog.perBind"/>
  </nm-list-dialog>
</template>
<script>
import { mixins } from 'nm-lib-skins'
import api from '../../../api/button'
import cols from './cols.js'
import PermissionBindPage from '../permission-bind'
export default {
  mixins: [mixins.dialog],
  components: { PermissionBindPage },
  data () {
    return {
      list: {
        noHeader: true,
        action: api.query,
        model: {
          menuId: '',
          name: ''
        },
        cols
      },
      curr: {},
      dialog: {
        perBind: false
      },
      removeAction: api.remove
    }
  },
  props: ['menu'],
  computed: {
    dialog_ () {
      return {
        title: `按钮管理--${this.menu.name}`,
        icon: 'detail',
        width: '70%',
        height: '80%'
      }
    },
    buttons () {
      let list = []
      // 根据菜单路由获取菜单的按钮列表
      const buttons = this.$router.options.routes.filter(item => item.name === this.menu.routeName && item.meta && item.meta.buttons)[0].meta.buttons

      if (buttons) {
        for (var key in buttons) {
          list.push(buttons[key])
        }
      }
      return list
    }
  },
  methods: {
    refresh () {
      if (this.$refs.list) { this.$refs.list.refresh() }
    },
    sync () {
      this._confirm('您确认要同步按钮信息吗', '同步按钮信息').then(() => {
        const model = { menuId: this.menu.id, buttons: [] }
        this.buttons.map(item => {
          model.buttons.push({ name: item.text, code: item.code, icon: item.icon })
        })
        api.sync(model).then(data => {
          this._success('同步成功')
          this.refresh()
        })
      })
    },
    bindPermission (row) {
      this.curr = row
      this.dialog.perBind = true
    },
    onOpen () {
      const hasChange = this.list.model.menuId !== this.menu.id
      this.list.model.menuId = this.menu.id
      if (this.list.model.menuId && hasChange) {
        this.refresh()
      }
    }
  }
}
</script>
