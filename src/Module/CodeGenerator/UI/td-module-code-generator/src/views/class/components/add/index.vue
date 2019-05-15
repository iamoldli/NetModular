<template>
  <td-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
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
        <el-form-item label="基类类型：" prop="baseEntityType">
          <td-select :method="getBaseEntityTypeSelect" v-model="form.model.baseEntityType"/>
        </el-form-item>
        <el-form-item label="备注：" prop="remarks">
          <el-input v-model="form.model.remarks"/>
        </el-form-item>
        <el-form-item label="基础方法：" prop="actions"></el-form-item>
      </el-col>
    </el-row>
  </td-form-dialog>
</template>
<script>
import api from '../../../../api/class.js'
import { mixins } from 'td-lib-skins'
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
          projectId: '',
          name: '',
          tableName: '',
          baseEntityType: 6,
          remarks: '',
          actions: []
        },
        rules: {
          projectId: [
            { required: true, message: '请选择项目', trigger: 'blur' }
          ],
          name: [
            { required: true, message: '请输入类名', trigger: 'blur' }
          ],
          remarks: [
            { required: true, message: '请输入备注', trigger: 'blur' }
          ],
          tableName: [
            { required: true, message: '请输入表名', trigger: 'blur' }
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
  },
  methods: {
    getBaseEntityTypeSelect () {
      return api.getBaseEntityTypeSelect()
    },
    onSuccess () {
      this.$emit('success')
    },
    onOpen () {
      this.$nextTick(() => {
        this.$refs.form.reset()
        this.form.model.projectId = this.project.id
      })
    }
  }
}
</script>
