<template>
  <nm-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="企业名称：" prop="enterpriseName">
          <el-input v-model="form.model.enterpriseName" clearable/>
        </el-form-item>
        <el-form-item label="联系人：" prop="contact">
          <el-input v-model="form.model.contact" clearable/>
        </el-form-item>
        <el-form-item label="结束日期：" prop="endDate">
          <el-input v-model="form.model.endDate" clearable/>
        </el-form-item>
        <el-form-item label="联系人手机号：" prop="contactPhone">
          <el-input v-model="form.model.contactPhone" clearable/>
        </el-form-item>
        <el-form-item label="离职原因：" prop="dimissionReason">
          <el-input v-model="form.model.dimissionReason" clearable/>
        </el-form-item>
        <el-form-item label="起始日期：" prop="startDate">
          <el-input v-model="form.model.startDate" clearable/>
        </el-form-item>
        <el-form-item label="职位：" prop="position">
          <el-input v-model="form.model.position" clearable/>
        </el-form-item>
        <el-form-item label="用户编号：" prop="userId">
          <el-input v-model="form.model.userId" clearable/>
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-dialog>
</template>
<script>
import api from '../../../../api/UserWorkHistory'
import { mixins } from 'nm-lib-skins'
export default {
  mixins: [mixins.dialog],
  data () {
    return {
      form: {
        title: '添加用户工作经历',
        icon: 'add',
        action: api.add,
        model: {
          /** 企业名称 */
          enterpriseName: '',
          /** 联系人 */
          contact: '',
          /** 结束日期 */
          endDate: '',
          /** 联系人手机号 */
          contactPhone: '',
          /** 离职原因 */
          dimissionReason: '',
          /** 起始日期 */
          startDate: '',
          /** 职位 */
          position: '',
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
