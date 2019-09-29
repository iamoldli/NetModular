<template>
  <nm-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="所属项目：">
          <el-input v-model="project.name" disabled />
        </el-form-item>
        <el-form-item label="实体名称：" prop="name">
          <el-input v-model="form.model.name" />
        </el-form-item>
        <el-form-item label="表名：" prop="tableName">
          <el-input v-model="form.model.tableName" />
        </el-form-item>
        <el-form-item label="基类类型：" prop="baseEntityType">
          <nm-select :method="getBaseEntityTypeSelect" v-model="form.model.baseEntityType" />
        </el-form-item>
        <el-form-item label="备注：" prop="remarks">
          <el-input v-model="form.model.remarks" />
        </el-form-item>
        <el-form-item label="基础方法：" prop="method">
          <el-checkbox v-model="form.model.method.add">Add</el-checkbox>
          <el-checkbox v-model="form.model.method.delete">Delete</el-checkbox>
          <el-checkbox v-model="form.model.method.edit">Edit</el-checkbox>
          <el-checkbox v-model="form.model.method.query">Query</el-checkbox>
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-dialog>
</template>
<script>
import api from '../../../../api/class.js'
import { mixins } from 'nm-lib-skins'
export default {
  mixins: [mixins.dialog],
  data() {
    return {
      form: {
        title: '添加实体',
        icon: 'add',
        width: '40%',
        action: api.add,
        model: {
          projectId: '',
          name: '',
          tableName: '',
          baseEntityType: 6,
          remarks: '',
          method: {
            query: true,
            add: true,
            delete: true,
            edit: true
          }
        },
        rules: {
          projectId: [
            { required: true, message: '请选择项目', trigger: 'blur' }
          ],
          name: [
            { required: true, message: '请输入实体名称', trigger: 'blur' }
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
    getBaseEntityTypeSelect() {
      return api.getBaseEntityTypeSelect()
    },
    onSuccess() {
      this.$emit('success')
    },
    onOpen() {
      this.$nextTick(() => {
        this.$refs.form.reset()
        this.form.model.projectId = this.project.id
      })
    }
  }
}
</script>
