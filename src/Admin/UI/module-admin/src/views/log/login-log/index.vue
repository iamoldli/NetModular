<template>
  <nm-container>
    <nm-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item label="账户编号：" prop="accountId">
          <el-input v-model="list.model.accountId" clearable />
        </el-form-item>
        <el-form-item label="登录平台：" prop="platform">
          <nm-platform-select v-model="list.model.platform" clearable />
        </el-form-item>
        <el-form-item label="登录方式：" prop="loginMode">
          <nm-loginmode-select v-model="list.model.loginMode" clearable />
        </el-form-item>
      </template>

      <template v-slot:header-toolbar>
        <nm-date-range-picker size="mini" :start.sync="list.model.startDate" :end.sync="list.model.endDate" @change="refresh" />
      </template>

      <template v-slot:col-success="{ row }">
        <el-tag v-if="row.success" type="success" effect="dark" size="mini">成功</el-tag>
        <el-tooltip v-else effect="dark" :content="row.error" placement="top">
          <el-tag type="danger" effect="dark" size="mini">失败</el-tag>
        </el-tooltip>
      </template>
    </nm-list>
  </nm-container>
</template>
<script>
import page from './page'
import cols from './cols'

// 接口
const api = $api.admin.log

export default {
  name: page.name,
  data() {
    return {
      list: {
        title: page.title,
        action: api.queryLogin,
        noOperation: true,
        model: {
          accountId: '',
          platform: '',
          loginMode: '',
          startDate: null,
          endDate: null
        },
        cols,
        /**导出配置 */
        exportOptions: {
          /**启用导出按钮 */
          enabled: true,
          /**导出数据的方法 */
          action: api.exportLogin,
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
      buttons: page.buttons
    }
  },
  methods: {
    refresh() {
      this.$refs.list.refresh()
    }
  }
}
</script>
