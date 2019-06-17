<template>
  <nm-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="名称：" prop="name">
          <el-input v-model="form.model.name" autofocus clearable/>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="地址：" prop="address">
          <el-input v-model="form.model.address" clearable/>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="10" :offset="1">
        <el-form-item label="联系人：" prop="contact">
          <el-input v-model="form.model.contact" clearable/>
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="联系电话：" prop="phone">
          <el-input v-model="form.model.phone" clearable/>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="传真：" prop="fax">
          <el-input v-model="form.model.fax" clearable/>
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-dialog>
</template>
<script>
import api from '../../../../api/Company'
import { mixins } from 'nm-lib-skins'
export default {
  mixins: [mixins.dialog],
  data() {
    return {
      form: {
        title: '添加公司单位',
        icon: 'add',
        width: '700px',
        action: api.add,
        model: {
          /** 名称 */
          name: '',
          /** 联系电话 */
          phone: '',
          /** 地址 */
          address: '',
          /** 联系人 */
          contact: '',
          /** 传真 */
          fax: ''
        },
        rules: {
          name: [{ required: true, message: '请输入名称' }],
          address: [{ required: true, message: '请输入地址' }]
        }
      },
      on: {
        success: this.onSuccess,
        open: this.onOpen
      }
    }
  },
  methods: {
    onSuccess() {
      this.$emit('success')
    },
    onOpen() {
      this.$nextTick(() => {
        this.$refs.form.reset()
      })
    }
  }
}
</script>
