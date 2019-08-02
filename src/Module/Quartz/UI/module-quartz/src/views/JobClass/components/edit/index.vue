<template>
  <nm-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="1">
      </el-col>
    </el-row>
  </nm-form-dialog>
</template>
<script>
import api from '../../../../api/JobClass'
import { mixins } from 'nm-lib-skins'
export default {
  mixins: [mixins.formDialogEdit],
  data () {
    return {
      api,
      form: {
        title: '编辑任务类型',
        action: api.update,
        model: {
        },
        rules: {
        }
      }
    }
  }
}
</script>
