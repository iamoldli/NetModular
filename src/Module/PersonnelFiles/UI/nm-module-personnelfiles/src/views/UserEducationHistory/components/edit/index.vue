<template>
  <nm-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="学历：" prop="level">
          <el-input v-model="form.model.level" clearable/>
        </el-form-item>
        <el-form-item label="学校名称：" prop="schoolName">
          <el-input v-model="form.model.schoolName" clearable/>
        </el-form-item>
        <el-form-item label="结束日期：" prop="endDate">
          <el-input v-model="form.model.endDate" clearable/>
        </el-form-item>
        <el-form-item label="开始日期：" prop="startDate">
          <el-input v-model="form.model.startDate" clearable/>
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-dialog>
</template>
<script>
import api from '../../../../api/UserEducationHistory'
import { mixins } from 'nm-lib-skins'
export default {
  mixins: [mixins.formDialogEdit],
  data () {
    return {
      api,
      form: {
        title: '编辑用户教育经历',
        action: api.update,
        model: {
          /** 学历 */
          level: '',
          /** 学校名称 */
          schoolName: '',
          /** 结束日期 */
          endDate: '',
          /** 开始日期 */
          startDate: ''
        },
        rules: {
        }
      }
    }
  }
}
</script>
