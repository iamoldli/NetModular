<template>
  <nm-form-page v-bind="form">
    <el-row>
      <el-col :span="10" :offset="1">
        <el-form-item label="Key前缀：" prop="prefix">
          <el-input v-model="form.model.prefix" />
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="连接字符串：" prop="connectionString">
          <el-input v-model="form.model.connectionString" placeholder="127.0.0.1" />
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
      code: 'Redis',
      form: {
        model: {
          prefix: '',
          connectionString: ''
        }
      }
    }
  }
}
</script>
