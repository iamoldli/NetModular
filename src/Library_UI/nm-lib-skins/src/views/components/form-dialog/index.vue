<template>
  <nm-container>
    <nm-box page header title="表单对话框(nm-form-dialog)" icon="edit">
      <nm-demo-desc>
        <p>表单对话框是将
          <nm-demo-link name="dialog" label="对话框"/>和
          <nm-demo-link name="form" label="基础表单"/>组合在一起，主要用于一些内容较少，不需要打开一个新页面操作的表单。
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
        title: '简单例子',
        height: 450,
        html: `  <section>
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
  </section>`,
        js: `export default {
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
}`
      }
    }
  }
}
</script>
