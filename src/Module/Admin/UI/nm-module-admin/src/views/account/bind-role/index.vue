<template>
  <nm-dialog title="绑定角色" icon="bind" :visible.sync="visible_">
    <role-checkbox :checked="checked"/>
  </nm-dialog>
</template>
<script>
import { mixins } from 'nm-lib-skins'
import RoleCheckbox from '../../role/checkbox'
export default {
  components: { RoleCheckbox },
  mixins: [mixins.dialog],
  data () {
    return {
      checked: []
    }
  },
  props: {
    // 账户编号
    id: {
      type: String,
      required: true
    },
    // 账户名称
    name: {
      type: String,
      required: true
    }
  },
  methods: {
    getChecked () {
    }
  }
}
</script>
