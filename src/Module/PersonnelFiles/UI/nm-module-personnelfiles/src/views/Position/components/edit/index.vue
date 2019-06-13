<template>
  <nm-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="部门编码：" prop="departmentId">
          <el-input v-model="form.model.departmentId" clearable/>
        </el-form-item>
        <el-form-item label="名称：" prop="name">
          <el-input v-model="form.model.name" clearable/>
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-dialog>
</template>
<script>
import api from '../../../../api/Position'
import { mixins } from 'nm-lib-skins'
export default {
  mixins: [mixins.formDialogEdit],
  data () {
    return {
      api,
      form: {
        title: '编辑岗位',
        action: api.update,
        model: {
          /** 部门编码 */
          departmentId: '',
          /** 名称 */
          name: ''
        },
        rules: {
        }
      }
    }
  }
}
</script>
