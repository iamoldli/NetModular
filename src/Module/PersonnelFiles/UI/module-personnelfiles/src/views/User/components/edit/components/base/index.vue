<template>
  <nm-form ref="form" v-bind="form" v-on="on">
    <el-row>
      <el-col :span="10" :offset="1">
        <el-form-item label="姓名：" prop="name">
          <el-input v-model="form.model.name" clearable/>
        </el-form-item>
        <el-form-item label="性别：" prop="sex">
          <el-radio-group v-model="form.model.sex">
            <el-radio :label="1">男</el-radio>
            <el-radio :label="2">女</el-radio>
          </el-radio-group>
        </el-form-item>
        <el-form-item label="出生日期：" prop="birthday">
          <el-date-picker v-model="form.model.birthday" value-format="yyyy-MM-dd" type="date" placeholder="出生日期"></el-date-picker>
        </el-form-item>
      </el-col>
      <el-col :span="6">
        <el-form-item label="照片：" prop="picture">
          <picture-upload v-model="form.model.picture"/>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="10" :offset="1">
        <el-form-item label="角色：" prop="roles">
          <nm-role-select ref="roles" multiple v-model="form.model.roles" :clearable="false" icon="role"/>
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="手机号：" prop="phone">
          <el-input v-model="form.model.phone" clearable>
            <nm-icon name="phone" slot="prefix"/>
          </el-input>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="10" :offset="1">
        <el-form-item label="部门：" prop="departmentId">
          <department-select ref="department" v-model="form.model.departmentId" :path="form.model.deptFullPath" clearable/>
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="岗位：" prop="positionId">
          <position-select :department-id="form.model.departmentId" v-model="form.model.positionId"/>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="10" :offset="1">
        <el-form-item label="民族：" prop="nation">
          <el-input v-model="form.model.nation" clearable/>
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="籍贯：" prop="native">
          <el-input v-model="form.model.native" clearable/>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="10" :offset="1">
        <el-form-item label="学历：" prop="education">
          <el-input v-model="form.model.education" clearable/>
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="身份证号：" prop="idCardNo">
          <el-input v-model="form.model.idCardNo" clearable>
            <nm-icon name="id" slot="prefix"/>
          </el-input>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="10" :offset="1">
        <el-form-item label="邮箱：" prop="email">
          <el-input v-model="form.model.email" clearable>
            <nm-icon name="email" slot="prefix"/>
          </el-input>
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form>
</template>
<script>
import mixins from '../mixins'
import api from '../../../../../../api/User'
import DepartmentSelect from '../../../../../Department/components/select'
import PositionSelect from '../../../../../Position/components/select'
import PictureUpload from '../../../picture-upload'

export default {
  mixins: [mixins],
  components: { DepartmentSelect, PositionSelect, PictureUpload },
  data() {
    return {
      editAction: api.edit,
      form: {
        action: api.update,
        model: {
          /** 姓名 */
          name: '',
          /** 性别 */
          sex: 1,
          /** 所属部门 */
          departmentId: '',
          /** 岗位编号 */
          positionId: '',
          /** 籍贯 */
          native: '',
          /** 出生日期 */
          birthday: '',
          /** 民族 */
          nation: '',
          /** 学历 */
          education: '',
          /** 照片 */
          picture: '',
          /** 身份证号 */
          idCardNo: '',
          /** 手机号 */
          phone: '',
          /** 邮箱 */
          email: '',
          roles: []
        },
        rules: {
          userName: [{ required: true, message: '请输入登录名', trigger: 'blur' }],
          password: [{ required: true, message: '请输入密码', trigger: 'blur' }, { min: 6, max: 30, message: '密码长度不能小于6位获大于30位', trigger: 'blur' }],
          name: [{ required: true, message: '请输入姓名', trigger: 'blur' }],
          departmentId: [{ required: true, message: '请选择部门', trigger: 'blur' }],
          positionId: [{ required: true, message: '请选择岗位', trigger: 'blur' }],
          phone: [{ pattern: /^1[345789]\d{9}$/, message: '手机号格式错误', trigger: 'blur' }],
          birthday: [{ required: true, message: '请选择出生日期', trigger: 'blur' }],
          email: [{ type: 'email', message: '请输入正确的邮箱', trigger: 'blur' }]
        }
      }
    }
  }
}
</script>
