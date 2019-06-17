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
        <el-form-item label="开始日期：" prop="startDate">
          <el-input v-model="form.model.startDate" clearable/>
        </el-form-item>
        <el-form-item label="结束日期：" prop="endDate">
          <el-input v-model="form.model.endDate" clearable/>
        </el-form-item>
        <el-form-item label="用户编号：" prop="userId">
          <el-input v-model="form.model.userId" clearable/>
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-dialog>
</template>
<script>
import api from '../../../../api/UserEducationHistory'
import { mixins } from 'nm-lib-skins'
export default {
  mixins: [mixins.dialog],
  data () {
    return {
      form: {
        title: '添加用户教育经历',
        icon: 'add',
        action: api.add,
        model: {
          /** 学历 */
          level: '',
          /** 学校名称 */
          schoolName: '',
          /** 开始日期 */
          startDate: '',
          /** 结束日期 */
          endDate: '',
          /** 用户编号 */
          userId: ''
        },
        rules: {
        }
      },
      on: {
        success: this.onSuccess,
        open: this.onOpen
      }
    }
  },
  methods: {
    onSuccess () {
      this.$emit('success')
    },
    onOpen () {
      this.$nextTick(() => {
        this.$refs.form.reset()
      })
    }
  }
}
</script>
