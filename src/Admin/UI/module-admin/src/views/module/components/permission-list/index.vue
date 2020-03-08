<template>
  <nm-list-dialog v-bind="dialog" :visible.sync="visible_" @open="onOpen">
    <template v-slot:title> 接口权限列表({{ module.name }}) </template>
    <nm-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item label="名称：" prop="name">
          <el-input v-model="list.model.name" clearable />
        </el-form-item>
      </template>
    </nm-list>
  </nm-list-dialog>
</template>
<script>
import { mixins } from 'netmodular-ui'
import cols from './cols'
const api = $api.admin.permission

export default {
  mixins: [mixins.visible],
  data() {
    return {
      dialog: {
        icon: 'permission',
        width: '900px'
      },
      list: {
        cols,
        noHeader: true,
        noOperation: true,
        action: api.query,
        queryOnCreated: false,
        model: {
          moduleCode: '',
          name: ''
        }
      }
    }
  },
  props: {
    module: Object
  },
  methods: {
    refresh() {
      this.$nextTick(() => {
        this.$refs.list.refresh()
      })
    },
    onOpen() {
      this.list.model.name = ''
      this.list.model.moduleCode = this.module.code
      this.refresh()
    }
  }
}
</script>
