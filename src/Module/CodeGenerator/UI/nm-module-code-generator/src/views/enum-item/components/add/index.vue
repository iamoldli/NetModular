<template>
  <nm-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="所属枚举：">
          <el-input :value="`${parent.remarks}(${parent.name})`" disabled/>
        </el-form-item>
        <el-form-item label="名称：" prop="name">
          <el-input v-model="form.model.name"/>
        </el-form-item>
        <el-form-item label="值：" prop="value">
          <el-input v-model.number="form.model.value"/>
        </el-form-item>
        <el-form-item label="备注：" prop="remarks">
          <el-input v-model="form.model.remarks"/>
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-dialog>
</template>
<script>
import api from '../../../../api/enum-item'
import { mixins } from 'nm-lib-skins'
export default {
  mixins: [mixins.dialog],
  data () {
    return {
      form: {
        title: '添加类',
        icon: 'add',
        width: '40%',
        action: api.add,
        model: {
          enumId: '',
          name: '',
          value: '',
          remarks: ''
        },
        rules: {
          enumId: [
            { required: true, message: '请选择枚举', trigger: 'blur' }
          ],
          name: [
            { required: true, message: '请输入名称', trigger: 'blur' }
          ],
          value: [
            { required: true, message: '请输入值', trigger: 'blur' },
            { type: 'number', message: '请输入值', trigger: 'blur' }
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
    parent: {
      type: Object,
      required: true
    }
  },
  methods: {
    onSuccess () {
      this.$emit('success')
    },
    onOpen () {
      this.$nextTick(() => {
        this.$refs.form.reset()
        this.form.model.enumId = this.parent.id
      })
    }
  }
}
</script>
