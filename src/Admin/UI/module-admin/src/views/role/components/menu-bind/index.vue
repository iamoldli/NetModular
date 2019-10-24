<template>
  <nm-dialog no-scrollbar width="70%" height="70%" title="绑定菜单" icon="bind" :visible.sync="visible_" :loading="loading">
    <menu-bind
      :id="id"
      :menu-query-action="menuQueryAction"
      :menu-update-action="menuUpdateAction"
      :button-query-action="buttonQueryAction"
      :button-update-action="buttonUpdateAction"
    />
  </nm-dialog>
</template>
<script>
import { mixins } from 'nm-lib-skins'
import MenuBind from '../../../menu/components/bind'

// 接口
const api = $api.admin.role

export default {
  components: { MenuBind },
  mixins: [mixins.dialog],
  data() {
    return {
      loading: false
    }
  },
  props: {
    id: {
      type: String,
      required: true
    }
  },
  methods: {
    async menuQueryAction() {
      let checkedKeys = []
      this.loading = true
      const data = await api.menuList(this.id)
      data.map(m => {
        if (m.menuType !== 0) {
          checkedKeys.push(m.menuId)
        }
      })
      this.loading = false
      return checkedKeys
    },
    async menuUpdateAction(menus) {
      this.loading = true
      await api.bindMenu({ id: this.id, menus })
      this.loading = false
    },
    async buttonQueryAction(menuId) {
      this.loading = true
      var data = await api.menuButtonList({ id: this.id, menuId })
      this.loading = false
      return data
    },
    buttonUpdateAction(params) {
      // params : {menuId, buttonId, checked} buttonId为空标识批量操作所有按钮

      this.loading = true
      api.bindMenuButton({ roleId: this.id, ...params })
      this.loading = false
    }
  }
}
</script>
