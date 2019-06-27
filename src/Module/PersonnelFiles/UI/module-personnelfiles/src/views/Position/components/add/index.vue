<template>
  <nm-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="部门：" prop="name">
          <el-input v-model="department.name" disabled/>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="10" :offset="1">
        <el-form-item label="名称：" prop="name">
          <el-input v-model="form.model.name" autofocus clearable/>
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="编码：" prop="code">
          <el-input v-model="form.model.code" clearable/>
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-dialog>
</template>
<script>
import api from '../../../../api/Position'
import { mixins } from 'nm-lib-skins'
export default {
  mixins: [mixins.dialog],
  data() {
    return {
      form: {
        title: '添加岗位',
        icon: 'add',
        action: api.add,
        model: {
          /** 部门编码 */
          departmentId: '',
          /** 名称 */
          name: '',
          code: ''
        },
        rules: {
          name: [{ required: true, message: '请输入名称' }]
        }
      },
      on: {
        success: this.onSuccess,
        open: this.onOpen
      }
    }
  },
  props: {
    department: {
      type: Object,
      required: true
    }
  },
  methods: {
    onSuccess() {
      this.$emit('success')
    },
    onOpen() {
      this.$nextTick(() => {
        this.$refs.form.reset()
        this.form.model.departmentId = this.department.id
      })
    }
  }
}
</script>
