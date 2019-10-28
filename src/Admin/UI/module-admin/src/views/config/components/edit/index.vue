<template>
  <nm-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="键：" prop="key">
          <el-input v-model="form.model.key" />
        </el-form-item>
        <el-form-item label="值：" prop="value">
          <el-input type="textarea" :rows="5" v-model="form.model.value"></el-input>
        </el-form-item>
        <el-form-item label="备注：" prop="remarks">
          <el-input type="textarea" v-model="form.model.remarks"></el-input>
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-dialog>
</template>
<script>
import { mixins } from 'netmodular-ui'

// 接口
const api = $api.admin.config

export default {
  mixins: [mixins.formDialogEdit],
  data() {
    return {
      api,
      form: {
        title: '编辑配置项',
        icon: 'edit',
        action: api.update,
        model: {
          id: '',
          key: '',
          value: '',
          remarks: ''
        },
        rules: {
          key: [
            { required: true, message: '请输入键', trigger: 'blur' }
          ],
          value: [
            { required: true, message: '请输入值', trigger: 'blur' }
          ]
        }
      }
    }
  }
}
</script>
