<template>
  <nm-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="2">
        <el-form-item label="名称" prop="name">
          <el-input v-model="form.model.name"/>
        </el-form-item>
        <el-form-item label="备注" prop="remarks">
          <el-input type="textarea" v-model="form.model.remarks"></el-input>
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-dialog>
</template>
<script>
import api from '../../../api/role'
import { mixins } from 'nm-lib-skins'
export default {
  mixins: [mixins.dialog],
  data () {
    return {
      form: {
        title: '编辑角色',
        icon: 'edit',
        width: '30%',
        action: api.update,
        model: {
          id: '',
          name: '',
          remarks: ''
        },
        rules: {
          name: [
            { required: true, message: '请输入名称', trigger: 'blur' }
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
