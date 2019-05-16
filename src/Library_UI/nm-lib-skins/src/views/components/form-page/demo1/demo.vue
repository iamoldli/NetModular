<template>
  <nm-form-page v-bind="form">
    <el-row>
      <el-col :span="21" :offset="1">
        <el-form-item label="姓名：" prop="name">
          <el-input v-model="form.model.name"/>
        </el-form-item>
      </el-col>
      <el-col :span="21" :offset="1">
        <el-form-item label="年龄：" prop="age">
          <el-input v-model="form.model.age"/>
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-page>
</template>
<script>
export default {
  data () {
    return {
      form: {
        header: true,
        title: '页表单示例',
        icon: 'add',
        model: {
          name: '',
          age: 0
        },
        rules: {
          name: [{ required: true, message: '请输入姓名' }]
        },
        action () {
          return new Promise(resolve => {
            resolve()
          })
        }
      }
    }
  }
}
</script>
