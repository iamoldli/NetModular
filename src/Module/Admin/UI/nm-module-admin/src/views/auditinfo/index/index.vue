<template>
  <nm-list ref="list" v-bind="list">
    <!--查询条件-->
    <template v-slot:querybar>
      <el-row :gutter="20">
        <el-col :span="11" :offset="1">
          <el-form-item label="模块：" prop="moduleCode">
            <module-info-select v-model="list.conditions.moduleCode"/>
          </el-form-item>
        </el-col>
        <el-col :span="11">
          <el-form-item label="账户：" prop="accountId">
            <el-input v-model="list.conditions.accountId"/>
          </el-form-item>
        </el-col>
      </el-row>
    </template>

    <template v-slot:col-executionDuration="{row}">
      <span>{{row.executionDuration}}ms</span>
    </template>

    <!--操作列-->
    <template v-slot:col-operation="{row}">
      <nm-button :text="buttons.details.text" type="text" @click="details(row)" v-nm-has="buttons.details"/>
    </template>

    <details-page :id="detailsPage.id" :visible.sync="detailsPage.visible"/>
  </nm-list>
</template>
<script>
import api from '../../../api/auditinfo'
import page from './page'
import cols from './cols.js'
import DetailsPage from '../details'
import ModuleInfoSelect from '../../moduleInfo/select/'

export default {
  name: page.name,
  components: { DetailsPage, ModuleInfoSelect },
  data () {
    return {
      list: {
        title: page.title,
        action: api.query,
        labelWidth: '60px',
        conditions: {
          accountId: '',
          moduleCode: ''
        },
        cols
      },
      detailsPage: {
        visible: false,
        id: 0
      },
      buttons: page.buttons
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
