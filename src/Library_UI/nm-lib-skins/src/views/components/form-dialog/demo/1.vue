<template>
  <section>
    <nm-button type="primary" @click="visible=true">添加</nm-button>
    <nm-form-dialog
      title="添加用户"
      icon="add"
      :model="model"
      :rules="rules"
      :action="action"
      :visible.sync="visible"
    >
      <el-form-item label="姓名：" prop="name">
        <el-input v-model="model.name"/>
      </el-form-item>
      <el-form-item label="年龄：" prop="age">
        <el-input type="age" v-model="model.age" autocomplete="off"></el-input>
      </el-form-item>
      <el-form-item label="区域">
        <el-select v-model="model.region" placeholder="请选择活动区域">
          <el-option label="区域一" value="shanghai"></el-option>
          <el-option label="区域二" value="beijing"></el-option>
        </el-select>
      </el-form-item>
    </nm-form-dialog>
  </section>
</template>
<script>
export default {
  data () {
    return {
      visible: false,
      model: {
        name: '',
        age: 0,
        region: ''
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
    }
  }
}
</script>
