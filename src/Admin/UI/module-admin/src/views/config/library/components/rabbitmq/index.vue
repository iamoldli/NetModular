<template>
  <nm-form-page v-bind="form">
    <el-row :gutter="20">
      <el-col :span="10" :offset="1">
        <el-form-item label="主机名：" prop="hostName">
          <el-input v-model="form.model.hostName" />
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="端口号：" prop="port">
          <el-input v-model.number="form.model.port" />
        </el-form-item>
      </el-col>
      <el-col :span="10" :offset="1">
        <el-form-item label="用户名：" prop="userName">
          <el-input v-model="form.model.userName" />
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="密码：" prop="password">
          <el-input v-model="form.model.password" />
        </el-form-item>
      </el-col>
      <el-col :span="10" :offset="1">
        <el-form-item label="虚拟目录：" prop="VirtualHost">
          <el-input v-model="form.model.VirtualHost" />
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="队列前缀：" prop="prefix">
          <el-input v-model="form.model.prefix" />
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-page>
</template>
<script>
import mixins from '../../form-mixins'
export default {
  mixins: [mixins],
  data() {
    return {
      code: 'RabbitMQ',
      form: {
        model: {
          hostName: 'localhost',
          port: 5672,
          userName: '',
          password: '',
          virtualHost: '',
          prefix: ''
        },
        rules: {
          port: [{ type: 'number', min: 1, message: '端口号不能小于1' }]
        }
      }
    }
  }
}
</script>
