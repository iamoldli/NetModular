<template>
  <nm-container>
    <nm-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item label="模块：" prop="moduleCode">
          <nm-module-select v-model="list.model.moduleCode" clearable />
        </el-form-item>
        <el-form-item label="文件名：" prop="fileName">
          <el-input v-model="list.model.fileName" clearable />
        </el-form-item>
      </template>

      <!--操作列-->
      <template v-slot:col-operation="{ row }">
        <nm-file-download v-bind="buttons.export" :url="row.url" :private="row.accessMode !== 1" @success="refresh" />
        <nm-button-delete v-bind="buttons.del" :id="row.id" :action="removeAction" @success="refresh" />
        <nm-button-delete v-bind="buttons.hardDel" :id="row.id" :action="hardRemoveAction" @success="refresh" />
      </template>
    </nm-list>
  </nm-container>
</template>
<script>
import { mixins } from 'netmodular-ui'
import page from './page'
import cols from './cols'

// 接口
const api = $api.admin.file

export default {
  name: page.name,
  mixins: [mixins.list],
  data() {
    return {
      list: {
        title: page.title,
        cols,
        action: api.query,
        operationWidth: '200',
        model: {
          moduleCode: '',
          fileName: ''
        }
      },
      removeAction: api.remove,
      hardRemoveAction: api.hardDelete,
      buttons: page.buttons
    }
  }
}
</script>
