<template>
  <nm-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="企业名称：" prop="enterpriseName">
          <el-input v-model="form.model.enterpriseName" autofocus clearable />
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="10" :offset="1">
        <el-form-item label="供职日期：" prop="startDate">
          <el-date-picker v-model="date" type="daterange" range-separator="至" start-placeholder="开始日期" end-placeholder="结束日期" value-format="yyyy-MM-dd"></el-date-picker>
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="职位：" prop="position">
          <el-input v-model="form.model.position" clearable />
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="10" :offset="1">
        <el-form-item label="联系人：" prop="contact">
          <el-input v-model="form.model.contact" clearable />
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="联系电话：" prop="contactPhone">
          <el-input v-model="form.model.contactPhone" clearable />
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="离职原因：" prop="dimissionReason">
          <el-input type="textarea" v-model="form.model.dimissionReason" clearable />
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-dialog>
</template>
<script>
import { mixins } from 'nm-lib-skins'

const api = $api.personnelFiles.userWorkHistory

export default {
  mixins: [mixins.formDialogEdit],
  data() {
    return {
      api,
      form: {
        title: '编辑工作经历',
        icon: 'edit',
        width: '800px',
        action: api.update,
        model: {
          id: '',
          enterpriseName: '',
          startDate: '',
          endDate: '',
          position: '',
          contact: '',
          contactPhone: '',
          dimissionReason: ''
        },
        rules: {
          userId: [{ required: true, message: '请选择用户', trigger: 'blur' }],
          enterpriseName: [{ required: true, message: '请输入企业名称', trigger: 'blur' }],
          startDate: [{ required: true, message: '请选择开始时间', trigger: 'blur' }],
          endDate: [{ required: true, message: '请选择结束时间', trigger: 'blur' }],
          contactPhone: [{ pattern: /^1[345789]\d{9}$/, message: '手机号格式错误', trigger: 'blur' }]
        }
      },
      date: []
    }
  },
  methods: {
    setDate() {
      const model = this.form.model
      if (model.startDate && model.endDate) {
        this.date = [model.startDate, model.endDate]
      } else {
        this.date = []
      }
    },
    afterRefresh() {
      this.setDate()
    },
    afterReset() {
      this.setDate()
    }
  },
  watch: {
    date(val) {
      const model = this.form.model
      if (val && val.length > 0) {
        model.startDate = val[0]
        model.endDate = val[1]
      } else {
        model.startDate = ''
        model.endDate = ''
      }
    }
  }
}
</script>
