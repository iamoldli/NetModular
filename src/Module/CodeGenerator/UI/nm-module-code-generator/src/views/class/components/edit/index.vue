<template>
  <nm-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="所属项目：">
          <el-input v-model="project.name" disabled/>
        </el-form-item>
        <el-form-item label="类名：" prop="name">
          <el-input v-model="form.model.name"/>
        </el-form-item>
        <el-form-item label="表名：" prop="tableName">
          <el-input v-model="form.model.tableName"/>
        </el-form-item>
        <el-form-item label="备注：" prop="remarks">
          <el-input v-model="form.model.remarks"/>
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-dialog>
</template>
<script>
import api from '../../../../api/class.js'
import { mixins } from 'nm-lib-skins'
export default {
  mixins: [mixins.formDialogEdit],
  data () {
    return {
      api,
      form: {
        title: '编辑类',
        width: '40%',
        action: api.update,
        model: {
          id: '',
          name: '',
          tableName: '',
          remarks: ''
        },
        rules: {
          name: [
            { required: true, message: '请输入类名', trigger: 'blur' }
          ],
          tableName: [
            { required: true, message: '请输入表名', trigger: 'blur' }
          ],
          remarks: [
            { required: true, message: '请输入备注', trigger: 'blur' }
          ]
        }
      },
      on: {
        success: this.onSuccess,
        open: this.onOpen
      }
    }
  },
  props: {
    project: {
      type: Object,
      required: true
    }
  }
}
</script>
