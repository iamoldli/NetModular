<template>
  <nm-container>
    <nm-form :form="form" @submit="submit">
      <el-form-item label="用户名：">
        <el-input v-model="form.name"></el-input>
      </el-form-item>
      <el-form-item label="年龄：">
        <el-input v-model="form.age"></el-input>
      </el-form-item>
      <el-form-item label="性别：">
        <el-radio-group v-model="form.sex">
          <el-radio label="男神"></el-radio>
          <el-radio label="女神"></el-radio>
        </el-radio-group>
      </el-form-item>
    </nm-form>
  </nm-container>
</template>
<script>
export default {
  name: 'UserAdd',
  data() {
    return {
      form: {
        name: '',
        age: 0,
        sex: 0
      }
    }
  },
  methods: {
    submit() {
    }
  }
}
</script>
