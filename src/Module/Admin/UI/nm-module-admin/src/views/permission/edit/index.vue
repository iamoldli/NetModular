<template>
  <nm-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="2">
        <el-form-item label="模块" prop="moduleCode">
          <module-info-select v-model="form.model.moduleCode"/>
        </el-form-item>
        <el-form-item label="名称" prop="name">
          <el-input v-model="form.model.name"></el-input>
        </el-form-item>
        <el-form-item label="控制器" prop="controller">
          <el-input v-model="form.model.controller"></el-input>
        </el-form-item>
        <el-form-item label="方法" prop="action">
          <el-input v-model="form.model.action"></el-input>
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-dialog>
</template>
<script>
import api from '../../../api/permission'
import { mixins } from 'nm-lib-skins'
import ModuleInfoSelect from '../../moduleInfo/select/'
export default {
  components: { ModuleInfoSelect },
  mixins: [mixins.dialog],
  data () {
    return {
      form: {
        title: '编辑权限',
        icon: 'add',
        width: '30%',
        action: api.update,
        model: {
          moduleCode: '',
          name: '',
          controller: '',
          action: ''
        },
        rules: {
          moduleCode: [
            { required: true, message: '请选择模块' }
          ],
          name: [
            { required: true, message: '请输入名称' }
          ],
          controller: [
            { required: true, message: '请输入控制器' }
          ],
          action: [
            { required: true, message: '请输入方法' }
          ]
        },
        loading: false
      },
      on: {
        success: this.onSuccess,
        open: this.onOpen
      }
    }
  },
  props: ['id'],
  methods: {
    async edit () {
      if (this.id === '') {
        this._warning('请选择要编辑的数据~')
        return
      }

      this.form.model.id = this.id

      this.form.loading = true
      this.form.model = await api.edit(this.form.model.id)
      this.form.loading = false
    },
    onSuccess () {
      this.$emit('success')
    },
    onOpen () {
      this.edit()
    }
  }
}
</script>
