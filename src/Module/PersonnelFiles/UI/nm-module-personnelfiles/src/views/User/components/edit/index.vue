<template>
  <nm-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="学历：" prop="education">
          <el-input v-model="form.model.education" clearable/>
        </el-form-item>
        <el-form-item label="职位编号：" prop="positionId">
          <el-input v-model="form.model.positionId" clearable/>
        </el-form-item>
        <el-form-item label="出生日期：" prop="birthday">
          <el-input v-model="form.model.birthday" clearable/>
        </el-form-item>
        <el-form-item label="工号：" prop="number">
          <el-input v-model="form.model.number" clearable/>
        </el-form-item>
        <el-form-item label="民族：" prop="nation">
          <el-input v-model="form.model.nation" clearable/>
        </el-form-item>
        <el-form-item label="性别：" prop="sex">
          <el-input v-model="form.model.sex" clearable/>
        </el-form-item>
        <el-form-item label="籍贯：" prop="native">
          <el-input v-model="form.model.native" clearable/>
        </el-form-item>
        <el-form-item label="所属部门：" prop="departmentId">
          <el-input v-model="form.model.departmentId" clearable/>
        </el-form-item>
        <el-form-item label="姓名：" prop="name">
          <el-input v-model="form.model.name" clearable/>
        </el-form-item>
        <el-form-item label="照片：" prop="picture">
          <el-input v-model="form.model.picture" clearable/>
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-dialog>
</template>
<script>
import api from '../../../../api/User'
import { mixins } from 'nm-lib-skins'
export default {
  mixins: [mixins.formDialogEdit],
  data () {
    return {
      api,
      form: {
        title: '编辑用户信息',
        action: api.update,
        model: {
          /** 学历 */
          education: '',
          /** 职位编号 */
          positionId: '',
          /** 出生日期 */
          birthday: '',
          /** 工号 */
          number: '',
          /** 民族 */
          nation: '',
          /** 性别 */
          sex: '',
          /** 籍贯 */
          native: '',
          /** 所属部门 */
          departmentId: '',
          /** 姓名 */
          name: '',
          /** 照片 */
          picture: ''
        },
        rules: {
        }
      }
    }
  }
}
</script>
