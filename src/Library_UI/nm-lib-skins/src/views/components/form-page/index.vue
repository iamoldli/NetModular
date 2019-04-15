<template>
  <nm-container>
    <nm-box page header title="表单页(nm-form)" icon="edit">
      <nm-demo-desc>
        <p>表单页是一个页面级组件，由一个
          <nm-demo-link name="box-page" label="盒子页"/>和一个
          <nm-demo-link name="form" label="基础表单"/>组成。
        </p>
        <p>表单页组件会填充整个内容区域，当内容超出时会自动显示滚动条</p>
      </nm-demo-desc>

      <nm-table-attributes :data="attrs"/>
      <nm-table-methods :data="methods"/>
      <nm-table-events :data="events"/>
      <nm-table-slots :data="slots"/>

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
import slots from './data/slots.js'
import demo1 from './demo/1'
export default {
  components: { demo1 },
  data () {
    return {
      attrs,
      methods,
      events,
      slots,
      demo1: {
        title: '基础表单',
        height: 300,
        html: `  <nm-form-page ref="myForm" title="添加用户" icon="add" :model="model" :rules="rules" :action="action">
    <el-form-item label="姓名：" prop="name">
      <el-input v-model="model.name"/>
    </el-form-item>
    <el-form-item label="年龄：" prop="age">
      <el-input type="age" v-model="model.age" autocomplete="off"></el-input>
    </el-form-item>
  </nm-form-page>`,
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
    }
  }
}`
      }
    }
  }
}
</script>
