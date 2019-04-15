<template>
  <nm-form ref="myForm" :model="model" :rules="rules" :action="action">
    <el-form-item label="姓名：" prop="name">
      <el-input v-model="model.name"/>
    </el-form-item>
    <el-form-item label="年龄：" prop="age">
      <el-input type="age" v-model="model.age" autocomplete="off"></el-input>
    </el-form-item>
    <el-form-item>
      <el-button type="primary" @click="submit">提交</el-button>
      <el-button @click="reset">重置</el-button>
    </el-form-item>
  </nm-form>
</template>
<script>
export default {
  data () {
    return {
      model: {
        name: '',
        age: 0
      },
      rules: {
        name: [
          { required: true, message: '请输入姓名' }
        ]
      }
    }
  },
  methods: {
    action () {
      return new Promise((resolve, reject) => {
        resolve(1)
      })
    },
    submit () {
      this.$refs.myForm.submit()
    },
    reset () {
      this.$refs.myForm.reset()
    }
  }
}
</script>
