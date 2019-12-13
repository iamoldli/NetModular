<template>
  <nm-container>
    <nm-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item label="模块：" prop="moduleCode">
          <module-info-select v-model="list.model.moduleCode" />
        </el-form-item>
        <el-form-item label="账户：" prop="accountId">
          <el-input v-model="list.model.accountId" />
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

      <template v-slot:col-moduleName="{ row }">
        <span>{{ row.moduleName }}({{ row.area }})</span>
      </template>

      <template v-slot:col-controller="{ row }">
        <span>{{ row.controllerDesc }}({{ row.controller }})</span>
      </template>

      <template v-slot:col-action="{ row }">
        <span>{{ row.actionDesc }}({{ row.action }})</span>
      </template>
      <template v-slot:col-executionDuration="{ row }">
        <span>{{ row.executionDuration }}ms</span>
      </template>

      <!--操作列-->
      <template v-slot:col-operation="{ row }">
        <nm-button v-bind="buttons.details" @click="details(row)" />
      </template>
    </nm-list>

    <details-page :id="detailsPage.id" :visible.sync="detailsPage.visible" />
  </nm-container>
</template>
<script>
import page from './page'
import cols from './cols'
import DetailsPage from '../components/details'
import ModuleInfoSelect from '../../moduleInfo/components/select'

// 接口
const api = $api.admin.auditInfo

export default {
  name: page.name,
  components: { DetailsPage, ModuleInfoSelect },
  data() {
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
        cols,
        /**导出配置 */
        exportOptions: {
          /**启用导出按钮 */
          enabled: true,
          /**导出数据的方法 */
          action: api.exportData,
          /**启用高级配置 */
          advanced: true,
          /**导出按钮位置 */
          btnLocation: 'querybar',
          /**导出按钮权限编码 */
          btnCode: page.buttons.export.code,
          /**默认是否显示标题 */
          showTitle: true,
          /**默认是否显示版权信息 */
          showCopyright: true,
          /**默认是否显示列名 */
          showColName: true,
          /**默认是否显示导出日期 */
          showExportDate: true,
          /**默认是否显示导出人 */
          showExportPeople: true
        }
      },
      detailsPage: {
        visible: false,
        id: 0
      },
      buttons: page.buttons
    }
  },
  methods: {
    refresh() {
      this.$refs.list.refresh()
    },
    details(row) {
      this.detailsPage.id = row.id
      this.detailsPage.visible = true
    }
  }
}
</script>
