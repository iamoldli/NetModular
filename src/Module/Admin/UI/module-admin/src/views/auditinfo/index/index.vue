<template>
  <nm-container>
    <nm-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item label="模块：" prop="moduleCode">
          <module-info-select v-model="list.model.moduleCode"/>
        </el-form-item>
        <el-form-item label="账户：" prop="accountId">
          <el-input v-model="list.model.accountId"/>
        </el-form-item>
      </template>
      <!--高级查询-->
      <template v-slot:querybar-advanced>
        <el-row>
          <el-col :span="20" :offset="1">
            <el-form-item label="开始时间：" prop="startTime">
              <el-date-picker v-model="list.model.startTime" type="date" placeholder="选择日期"></el-date-picker>
            </el-form-item>
            <el-form-item label="结束时间：" prop="startTime">
              <el-date-picker v-model="list.model.endTime" type="date" placeholder="选择日期"></el-date-picker>
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
    </nm-list>

    <details-page :id="detailsPage.id" :visible.sync="detailsPage.visible"/>
  </nm-container>
</template>
<script>
import api from '../../../api/auditinfo'
import page from './page'
import cols from './cols.js'
import DetailsPage from '../components/details'
import ModuleInfoSelect from '../../moduleInfo/components/select'

export default {
  name: page.name,
  components: { DetailsPage, ModuleInfoSelect },
  data () {
    return {
      list: {
        title: page.title,
        action: api.query,
        labelWidth: '60px',
        advanced: {
          enabled: true,
          labelWidth: '100px'
        },
        model: {
          accountId: '',
          moduleCode: '',
          startTime: '',
          endTime: ''
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
