<template>
  <nm-dialog v-bind="dialog" :visible.sync="visible_" @open="onOpen">
    <bind-page ref="bind" :query="query" :action="action"/>
  </nm-dialog>
</template>
<script>
import { mixins } from 'nm-lib-skins'
import BindPage from '../../permission/components/bind'
import api from '../../../api/button'
export default {
  mixins: [mixins.dialog],
  components: { BindPage },
  data () {
    return {
      id_: ''
    }
  },
  props: ['id', 'name'],
  computed: {
    dialog () {
      return {
        noScrollbar: true,
        noFooter: true,
        title: `权限绑定(${this.name})`,
        icon: 'bind',
        width: '70%',
        height: '70%'
      }
    }
  },
  methods: {
    query () {
      return api.getPermissionList(this.id)
    },
    action (list) {
      return api.bindPermission({ id: this.id, permissionList: list })
    },
    onOpen () {
      if (this.id && this.id !== this.id_) {
        this.$nextTick(() => {
          this.$refs.bind.refresh()
        })
      }
      this.id_ = this.id
    }
  }
}
</script>
