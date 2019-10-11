<template>
  <nm-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="部门：" prop="name">
          <el-input v-model="department.name" disabled />
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="10" :offset="1">
        <el-form-item label="名称：" prop="name">
          <el-input v-model="form.model.name" autofocus clearable />
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="编码：" prop="code">
          <el-input v-model="form.model.code" clearable />
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-dialog>
</template>
<script>
import { mixins } from 'nm-lib-skins'

const api = $api.personnelFiles.position

export default {
  mixins: [mixins.formDialogEdit],
  data() {
    return {
      api,
      form: {
        title: '编辑岗位',
        action: api.update,
        model: {
          /** 部门编码 */
          id: '',
          /** 名称 */
          name: '',
          code: ''
        },
        rules: {
        }
      }
    }
  },
  props: {
    department: {
      type: Object,
      required: true
    }
  }
}
</script>
