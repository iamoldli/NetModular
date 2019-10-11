<template>
  <nm-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="名称：" prop="name">
          <el-input v-model="form.model.name" />
        </el-form-item>
        <el-form-item label="编号：" prop="no">
          <el-input v-model.number="form.model.no" />
        </el-form-item>
        <el-form-item label="前缀：" prop="prefix">
          <el-input v-model="form.model.prefix" />
        </el-form-item>
        <el-form-item label="编码：" prop="code">
          <el-input v-model="form.model.code" />
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-dialog>
</template>
<script>
import { mixins } from 'nm-lib-skins'

const api = $api.codeGenerator.project

export default {
  mixins: [mixins.formDialogEdit],
  data() {
    return {
      api,
      form: {
        title: '编辑项目',
        width: '30%',
        action: api.update,
        labelWidth: '70px',
        model: {
          name: '',
          no: 0,
          prefix: '',
          code: ''
        },
        rules: {
          name: [
            { required: true, message: '请输入名称', trigger: 'blur' }
          ],
          code: [
            { required: true, message: '请输入编码', trigger: 'blur' }
          ]
        }
      }
    }
  }
}
</script>
