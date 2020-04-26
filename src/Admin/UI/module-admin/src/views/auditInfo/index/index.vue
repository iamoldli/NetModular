<template>
  <nm-container>
    <nm-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item label="账户编号：" prop="accountId">
          <el-input v-model="list.model.accountId" clearable />
        </el-form-item>
        <el-form-item label="模块：" prop="moduleCode">
          <nm-module-select v-model="list.model.moduleCode" clearable />
        </el-form-item>
        <el-form-item label="控制器：" prop="controller">
          <el-input v-model="list.model.controller" clearable />
        </el-form-item>
        <el-form-item label="操作：" prop="action">
          <el-input v-model="list.model.action" clearable />
        </el-form-item>
        <el-form-item label="访问来源：" prop="platform">
          <nm-platform-select v-model="list.model.platform" clearable />
        </el-form-item>
      </template>

      <template v-slot:header-toolbar>
        <nm-date-range-picker size="mini" :start.sync="list.model.startDate" :end.sync="list.model.endDate" @change="refresh" />
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

// 接口
const api = $api.admin.auditInfo

export default {
  name: page.name,
  components: { DetailsPage },
  data() {
    return {
      list: {
        title: page.title,
        action: api.query,
        model: {
          accountId: '',
          moduleCode: '',
          controller: '',
          action: '',
          platform: '',
          startDate: null,
          endDate: null
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
