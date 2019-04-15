<template>
  <nm-container>
    <nm-box page header title="基础表单(nm-form)" icon="edit">
      <nm-demo-desc>
        <p>
          基础表单是对
          <code>el-form</code>组件的封装，预定义了loading样式以及添加了一些简单的扩展。其他表单相关组件依赖该组件，比如
          <nm-demo-link name="form-dialog" label="对话框表单"/>和
          <nm-demo-link name="form-page" label="页表单"/>
        </p>
      </nm-demo-desc>

      <nm-table-attributes :data="attrs"/>
      <nm-table-methods :data="methods"/>
      <nm-table-events :data="events"/>

      <nm-demo :options="demo1">
        <demo1/>
      </nm-demo>
    </nm-box>
  </nm-container>
</template>
<script>
import attrs from './data/attrs.js'
import methods from './data/methods.js'
import events from './data/events.js'
import demo1 from './demo/1'
export default {
  components: { demo1 },
  data () {
    return {
      attrs,
      methods,
      events,
      demo1: {
        title: '基础表单',
        height: 300,
        html: `  <nm-form ref="myForm" :model="model" :rules="rules" :action="action">
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
  </nm-form>`,
        js: `export default {
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
}`
      }
    }
  }
}
</script>
