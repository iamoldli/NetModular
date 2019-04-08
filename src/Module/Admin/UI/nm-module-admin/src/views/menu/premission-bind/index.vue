<template>
  <nm-dialog v-bind="dialog" :visible.sync="visible_">
    <bind-page ref="permission" :id="currentMenu.id" :query="query" :action="action"/>
  </nm-dialog>
</template>
<script>
import { mapState } from 'vuex'
import { mixins } from 'nm-lib-skins'
import BindPage from '../../permission/bind'
import api from '../../../api/menu.js'
export default {
  mixins: [mixins.dialog],
  components: { BindPage },
  data () {
    return {
      query: api.permissionList,
      action: api.bindPermission
    }
  },
  computed: {
    ...mapState('module/admin', ['currentMenu']),
    dialog () {
      return {
        noScrollbar: true,
        noFooter: true,
        title: `权限绑定(${this.currentMenu.name})`,
        icon: 'bind',
        width: '70%',
        height: '70%'
      }
    }
  }
}
</script>
