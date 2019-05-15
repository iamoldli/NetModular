<template>
  <td-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="名称：" prop="name">
          <el-input v-model="form.model.name"/>
        </el-form-item>
        <el-form-item label="前缀：" prop="prefix">
          <el-input v-model="form.model.prefix"/>
        </el-form-item>
        <el-form-item label="编码：" prop="code">
          <el-input v-model="form.model.code"/>
        </el-form-item>
      </el-col>
    </el-row>
  </td-form-dialog>
</template>
<script>
import api from '../../../../api/project.js'
import { mixins } from 'td-lib-skins'
export default {
  mixins: [mixins.dialog],
  data () {
    return {
      form: {
        title: '添加项目',
        icon: 'add',
        width: '30%',
        action: api.add,
        labelWidth: '70px',
        model: {
          name: '',
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
