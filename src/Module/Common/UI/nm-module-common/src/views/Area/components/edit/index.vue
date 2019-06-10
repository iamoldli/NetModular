<template>
  <nm-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="级别：" prop="level">
          <el-input v-model="form.model.level" clearable/>
        </el-form-item>
        <el-form-item label="纬度：" prop="latitude">
          <el-input v-model="form.model.latitude" clearable/>
        </el-form-item>
        <el-form-item label="拼音：" prop="pinyin">
          <el-input v-model="form.model.pinyin" clearable/>
        </el-form-item>
        <el-form-item label="区号：" prop="areaCode">
          <el-input v-model="form.model.areaCode" clearable/>
        </el-form-item>
        <el-form-item label="邮编：" prop="zipCode">
          <el-input v-model="form.model.zipCode" clearable/>
        </el-form-item>
        <el-form-item label="精度：" prop="longitude">
          <el-input v-model="form.model.longitude" clearable/>
        </el-form-item>
        <el-form-item label="简称：" prop="shortName">
          <el-input v-model="form.model.shortName" clearable/>
        </el-form-item>
        <el-form-item label="名称：" prop="name">
          <el-input v-model="form.model.name" clearable/>
        </el-form-item>
        <el-form-item label="简拼：" prop="jianpin">
          <el-input v-model="form.model.jianpin" clearable/>
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-dialog>
</template>
<script>
import api from '../../../../api/Area'
import { mixins } from 'nm-lib-skins'
export default {
  mixins: [mixins.formDialogEdit],
  data () {
    return {
      api,
      form: {
        title: '编辑区划代码',
        action: api.update,
        model: {
          /** 级别 */
          level: '',
          /** 纬度 */
          latitude: '',
          /** 拼音 */
          pinyin: '',
          /** 区号 */
          areaCode: '',
          /** 邮编 */
          zipCode: '',
          /** 精度 */
          longitude: '',
          /** 简称 */
          shortName: '',
          /** 名称 */
          name: '',
          /** 简拼 */
          jianpin: ''
        },
        rules: {
        }
      }
    }
  }
}
</script>
