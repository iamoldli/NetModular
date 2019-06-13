<template>
  <nm-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="详细地址：" prop="address">
          <el-input v-model="form.model.address" clearable/>
        </el-form-item>
        <el-form-item label="微信：" prop="wechat">
          <el-input v-model="form.model.wechat" clearable/>
        </el-form-item>
        <el-form-item label="省份编码：" prop="provinceCode">
          <el-input v-model="form.model.provinceCode" clearable/>
        </el-form-item>
        <el-form-item label="城镇编码：" prop="townCode">
          <el-input v-model="form.model.townCode" clearable/>
        </el-form-item>
        <el-form-item label="邮箱：" prop="email">
          <el-input v-model="form.model.email" clearable/>
        </el-form-item>
        <el-form-item label="钉钉：" prop="dingDing">
          <el-input v-model="form.model.dingDing" clearable/>
        </el-form-item>
        <el-form-item label="QQ：" prop="qQ">
          <el-input v-model="form.model.qQ" clearable/>
        </el-form-item>
        <el-form-item label="手机号：" prop="phone">
          <el-input v-model="form.model.phone" clearable/>
        </el-form-item>
        <el-form-item label="城市编码：" prop="cityCode">
          <el-input v-model="form.model.cityCode" clearable/>
        </el-form-item>
        <el-form-item label="用户编号：" prop="userId">
          <el-input v-model="form.model.userId" clearable/>
        </el-form-item>
        <el-form-item label="区县编码：" prop="areaCode">
          <el-input v-model="form.model.areaCode" clearable/>
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-dialog>
</template>
<script>
import api from '../../../../api/UserContact'
import { mixins } from 'nm-lib-skins'
export default {
  mixins: [mixins.formDialogEdit],
  data () {
    return {
      api,
      form: {
        title: '编辑用户联系信息',
        action: api.update,
        model: {
          /** 详细地址 */
          address: '',
          /** 微信 */
          wechat: '',
          /** 省份编码 */
          provinceCode: '',
          /** 城镇编码 */
          townCode: '',
          /** 邮箱 */
          email: '',
          /** 钉钉 */
          dingDing: '',
          /** QQ */
          qQ: '',
          /** 手机号 */
          phone: '',
          /** 城市编码 */
          cityCode: '',
          /** 用户编号 */
          userId: '',
          /** 区县编码 */
          areaCode: ''
        },
        rules: {
        }
      }
    }
  }
}
</script>
