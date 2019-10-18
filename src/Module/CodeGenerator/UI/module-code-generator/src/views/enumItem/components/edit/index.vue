<template>
  <nm-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="所属枚举：">
          <el-input :value="`${parent.remarks}(${parent.name})`" disabled />
        </el-form-item>
        <el-form-item label="名称：" prop="name">
          <el-input v-model="form.model.name" />
        </el-form-item>
        <el-form-item label="值：" prop="value">
          <el-input v-model.number="form.model.value" />
        </el-form-item>
        <el-form-item label="备注：" prop="remarks">
          <el-input v-model="form.model.remarks" />
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-dialog>
</template>
<script>
import { mixins } from 'nm-lib-skins'

const api = $api.codeGenerator.enumItem

export default {
  mixins: [mixins.formDialogEdit],
  data() {
    return {
      api,
      form: {
        title: '编辑枚举项',
        width: '40%',
        action: api.update,
        model: {
          name: '',
          value: '',
          remarks: ''
        },
        rules: {
          name: [
            { required: true, message: '请输入名称', trigger: 'blur' }
          ],
          value: [
            { required: true, message: '请输入值', trigger: 'blur' },
            { type: 'number', message: '请输入正确的数值', trigger: 'blur' }
          ],
          remarks: [
            { required: true, message: '请输入备注', trigger: 'blur' }
          ]
        }
      },
      on: {
        opened: this.opened
      }
    }
  },
  props: {
    parent: {
      type: Object,
      required: true
    }
  },
  methods: {
    opened() {
      console.log(this.form)
    }
  }
}
</script>
