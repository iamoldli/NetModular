<template>
  <nm-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="父节点：" prop="parentId">
          <el-input v-model="fullPath" disabled />
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="10" :offset="1">
        <el-form-item label="名称：" prop="name">
          <el-input v-model="form.model.name" clearable />
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="编码：" prop="code">
          <el-input v-model="form.model.code" clearable />
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="10" :offset="1">
        <el-form-item label="精度：" prop="longitude">
          <el-input v-model="form.model.longitude" clearable />
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="纬度：" prop="latitude">
          <el-input v-model="form.model.latitude" clearable />
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="10" :offset="1">
        <el-form-item label="邮编：" prop="zipCode">
          <el-input v-model="form.model.zipCode" clearable />
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="区号：" prop="areaCode">
          <el-input v-model="form.model.areaCode" clearable />
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-dialog>
</template>
<script>
import { mixins } from 'nm-lib-skins'

// 接口
const api = $api.common.area

export default {
  mixins: [mixins.dialog],
  data() {
    return {
      form: {
        title: '添加区划代码',
        icon: 'add',
        action: api.add,
        model: {
          /** 父节点 */
          parentId: '',
          /** 名称 */
          name: '',
          /** 编码 */
          code: '',
          /** 精度 */
          longitude: '',
          /** 纬度 */
          latitude: '',
          /** 邮编 */
          zipCode: '',
          /** 区号 */
          areaCode: ''
        },
        rules: {
          parentId: [
            { required: true, message: '请选择父节点' }
          ],
          name: [
            { required: true, message: '请输入名称' }
          ],
          code: [
            { required: true, message: '请输入编码' }
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
    parentId: {
      type: Number
    },
    fullPath: {
      type: String
    }
  },
  methods: {
    onSuccess() {
      this.$emit('success')
    },
    onOpen() {
      this.$nextTick(() => {
        this.$refs.form.reset()
        this.form.model.parentId = this.parentId
      })
    }
  }
}
</script>
