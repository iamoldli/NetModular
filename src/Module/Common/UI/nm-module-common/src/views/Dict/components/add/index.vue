<template>
  <nm-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="父节点：" prop="parentId">
          <el-input v-model="form.model.parentId" clearable/>
        </el-form-item>
        <el-form-item label="排序：" prop="sort">
          <el-input v-model="form.model.sort" clearable/>
        </el-form-item>
        <el-form-item label="名称：" prop="name">
          <el-input v-model="form.model.name" clearable/>
        </el-form-item>
        <el-form-item label="值：" prop="value">
          <el-input v-model="form.model.value" clearable/>
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-dialog>
</template>
<script>
import api from '../../../../api/Dict'
import { mixins } from 'nm-lib-skins'
export default {
  mixins: [mixins.dialog],
  data () {
    return {
      form: {
        title: '添加字典',
        icon: 'add',
        action: api.add,
        model: {
          /** 父节点 */
          parentId: '',
          /** 排序 */
          sort: '',
          /** 名称 */
          name: '',
          /** 值 */
          value: ''
        },
        rules: {
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
