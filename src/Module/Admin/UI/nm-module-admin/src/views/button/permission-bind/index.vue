<template>
  <nm-dialog no-footer :title="title" icon="bind" width="70%" height="70%" no-scrollbar :visible.sync="visible_" show-fullscreen show-refresh @refresh="refresh">
    <bind-page ref="permission" :id="button.id" :query="query" :action="action"/>
  </nm-dialog>
</template>
<script>
import { mixins } from 'nm-lib-skins'
import BindPage from '../../permission/bind'
import api from '../../../api/button.js'
export default {
  mixins: [mixins.dialog],
  components: { BindPage },
  data () {
    return {
      query: api.getPermissionList,
      action: api.bindPermission
    }
  },
  props: ['button'],
  computed: {
    title () {
      return `权限绑定(${this.button.name})`
    }
  },
  methods: {
    refresh () {
      this.$refs.permission.querySelection()
    }
  }
}
</script>
