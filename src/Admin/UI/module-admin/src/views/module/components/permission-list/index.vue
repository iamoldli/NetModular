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

      <template v-slot:col-httpMethod="{ row }"> {{ getHttpMethodName(row.httpMethod) }} </template>
    </nm-list>
  </nm-list-dialog>
</template>
<script>
import { mixins } from 'netmodular-ui'
import cols from './cols'
const { query } = $api.admin.permission

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
        action: this.query,
        queryOnCreated: false,
        noFooter: true,
        model: {
          name: ''
        }
      },
      rows: []
    }
  },
  props: {
    module: Object
  },
  methods: {
    refresh() {
      query({ moduleCode: this.module.code }).then(rows => {
        this.rows = rows
        this.$refs.list.refresh()
      })
    },
    query() {
      const name = this.list.model.name
      const rows = name ? this.rows.filter(m => m.name.includes(name)) : this.rows
      return new Promise(resolve => {
        resolve({
          rows: rows,
          total: rows.length
        })
      })
    },
    onOpen() {
      this.list.model.name = ''
      this.list.model.moduleCode = this.module.code
      this.refresh()
    },
    getHttpMethodName(httpMethod) {
      switch (httpMethod) {
        case 0:
          return 'GET'
        case 1:
          return 'PUT'
        case 2:
          return 'POST'
        case 3:
          return 'DELETE'
        case 4:
          return 'HEAD'
        case 5:
          return 'OPTIONS'
        case 6:
          return 'TRACE'
        case 7:
          return 'PATCH'
      }
    }
  }
}
</script>
