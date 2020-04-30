<template>
  <nm-container>
    <nm-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item label="扩展名：" prop="ext">
          <el-input v-model="list.model.ext" clearable />
        </el-form-item>
      </template>

      <!--按钮-->
      <template v-slot:querybar-buttons>
        <nm-button v-bind="buttons.add" @click="add" />
      </template>

      <!--操作列-->
      <template v-slot:col-operation="{ row }">
        <nm-button v-bind="buttons.edit" @click="edit(row)" />
        <nm-button-delete v-bind="buttons.del" :id="row.id" :action="removeAction" @success="refresh" />
      </template>
    </nm-list>

    <!--编辑-->
    <save-page :id="curr.id" :visible.sync="dialog.save" @success="refresh" />
  </nm-container>
</template>
<script>
import { mixins } from 'netmodular-ui'
import page from './page'
import cols from './cols'
import SavePage from '../components/save'

// 接口
const api = $api.admin.mime

export default {
  name: page.name,
  mixins: [mixins.list],
  components: { SavePage },
  data() {
    return {
      list: {
        title: page.title,
        cols,
        action: api.query,
        model: {
          ext: ''
        }
      },
      removeAction: api.remove,
      buttons: page.buttons
    }
  }
}
</script>
