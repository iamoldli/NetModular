<template>
  <nm-list ref="list" v-bind="list">
    <!--查询条件-->
    <template v-slot:querybar>
      <el-form-item label="账户：" prop="accountId">
        <el-input v-model="list.conditions.accountId"/>
      </el-form-item>
      <el-form-item label="模块：" prop="moduleCode">
        <el-input v-model="list.conditions.moduleCode"/>
      </el-form-item>
    </template>

    <template v-slot:col-executionDuration="{row}">
      <span>{{row.executionDuration}}ms</span>
    </template>

    <!--操作列-->
    <template v-slot:col-operation="{row}">
      <nm-button text="详情" icon="detail" type="text" @click="details(row)"/>
    </template>

    <details-page :id="detailsPage.id" :visible.sync="detailsPage.visible"/>
  </nm-list>
</template>
<script>
import api from '../../../api/auditinfo'
import page from './page'
import cols from './cols.js'
import DetailsPage from '../details'

export default {
  name: page.name,
  components: { DetailsPage },
  data () {
    return {
      list: {
        title: page.title,
        action: api.query,
        conditions: {
          accountId: '',
          moduleCode: ''
        },
        cols
      },
      detailsPage: {
        visible: false,
        id: 0
      }
    }
  },
  methods: {
    refresh () {
      this.$refs.list.refresh()
    },
    details (row) {
      this.detailsPage.id = row.id
      this.detailsPage.visible = true
    }
  }
}
</script>
