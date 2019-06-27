<template>
  <nm-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="名称：" prop="name">
          <el-input v-model="form.model.name" clearable/>
        </el-form-item>
        <el-form-item label="排序：" prop="sort">
          <el-input v-model="form.model.sort" clearable/>
        </el-form-item>
        <el-form-item label="值：" prop="value">
          <el-input v-model="form.model.value" clearable/>
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-dialog>
</template>
<script>
import api from '../../../../api/Dict'
import { mixins } from 'nm-lib-skins'
export default {
  mixins: [mixins.formDialogEdit],
  data () {
    return {
      api,
      form: {
        title: '编辑字典',
        action: api.update,
        model: {
          /** 名称 */
          name: '',
          /** 排序 */
          sort: '',
          /** 值 */
          value: ''
        },
        rules: {
        }
      }
    }
  }
}
</script>
