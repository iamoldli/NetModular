<template>
  <nm-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="用户名：" prop="userName">
          <el-input v-model="form.model.userName" />
        </el-form-item>
        <el-form-item label="角色：" prop="roles">
          <role-select ref="roles" multiple v-model="form.model.roles" :clearable="false" />
        </el-form-item>
        <el-form-item label="名称：" prop="name">
          <el-input v-model="form.model.name" />
        </el-form-item>
        <el-form-item label="密码：" prop="password">
          <el-input v-model="form.model.password" placeholder="默认密码123456" clearable />
        </el-form-item>
        <el-form-item label="手机：" prop="phone">
          <el-input type="number" v-model="form.model.phone" clearable />
        </el-form-item>
        <el-form-item label="邮箱：" prop="email">
          <el-input type="email" v-model="form.model.email" clearable />
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-dialog>
</template>
<script>
import { mixins } from 'netmodular-ui'
import RoleSelect from '../../../role/components/select'

// 接口
const api = $api.admin.account

export default {
  mixins: [mixins.dialog],
  components: { RoleSelect },
  data() {
    return {
      form: {
        title: '添加账户',
        icon: 'add',
        width: '500px',
        action: api.add,
        model: {
          name: '',
          roles: [],
          userName: '',
          password: '',
          phone: '',
          email: ''
        },
        rules: {
          name: [
            { required: true, message: '请输入名称' }
          ],
          userName: [
            { required: true, message: '请输入用户名' }
          ],
          password: [
            { min: 6, message: '密码长度不能小于6位' }
          ],
          phone: [
            { len: 11, message: '请输入正确的手机号' }
          ],
          email: [
            { type: 'email', message: '请输入正确的邮箱地址' }
          ]
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
        this.$refs.roles.refresh()
        this.$refs.form.reset()
      })
    }
  }
}
</script>
