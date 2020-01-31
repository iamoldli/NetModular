<template>
  <nm-list ref="list" v-bind="list">
    <!--查询条件-->
    <template v-slot:querybar>
      <el-form-item label="配置键或值：" prop="key">
        <el-input v-model="list.model.key" clearable />
      </el-form-item>
    </template>

    <!--按钮-->
    <template v-slot:querybar-buttons>
      <nm-button v-bind="buttons.add" @click="add" />
    </template>

    <!--操作列-->
    <template v-slot:col-operation="{ row }">
      <nm-button v-bind="buttons.edit" @click="edit(row)" />
      <nm-button-delete v-bind="buttons.del" :action="removeAction" :id="row.id" @success="refresh" />
    </template>

    <!--编辑页-->
    <save-page :id="curr.id" :visible.sync="dialog.save" @success="refresh" />
  </nm-list>
</template>
<script>
import { mixins } from 'netmodular-ui'
import page from '../page'
import cols from './cols'
import SavePage from '../../components/save'

// 接口
const api = $api.admin.config

export default {
  name: page.name,
  mixins: [mixins.list],
  components: { SavePage },
  data() {
    return {
      list: {
        noHeader: true,
        cols,
        action: api.query,
        model: {
          key: ''
        }
      },
      removeAction: api.remove,
      buttons: page.buttons
    }
  }
}
</script>
