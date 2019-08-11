<template>
  <nm-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="院校名称：" prop="schoolName">
          <el-input v-model="form.model.schoolName" autofocus clearable/>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="日期：" prop="startDate">
          <el-date-picker v-model="date" type="monthrange" range-separator="至" start-placeholder="开始月份" end-placeholder="结束月份" value-format="yyyy-MM"></el-date-picker>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="学历：" prop="level">
          <el-input v-model="form.model.level" clearable/>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="专业：" prop="profession">
          <el-input v-model="form.model.profession" clearable/>
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
  data() {
    return {
      api,
      form: {
        title: '编辑教育经历',
        width: '500px',
        action: api.update,
        model: {
          userId: '',
          schoolName: '',
          level: '',
          startDate: '',
          endDate: '',
          profession: ''
        },
        rules: {
          userId: [{ required: true, message: '请选择用户', trigger: 'blur' }],
          schoolName: [{ required: true, message: '请输入院校名称', trigger: 'blur' }],
          startDate: [{ required: true, message: '请选择开始时间', trigger: 'blur' }],
          endDate: [{ required: true, message: '请选择结束时间', trigger: 'blur' }]
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
