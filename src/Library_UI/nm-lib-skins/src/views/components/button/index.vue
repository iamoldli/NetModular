<template>
  <nm-container>
    <nm-box page header title="按钮(nm-button)" icon="app">
      <nm-demo-desc>
        <p>
          按钮是对ElementUI的
          <code>el-button</code>组件的封装，用法基本一致的，主要区别是按钮图标的用法，该组件用的是框架自己的图表库，需要通过
          <code>icon</code>属性来指定
        </p>
      </nm-demo-desc>

      <nm-demo :options="demo1">
        <el-row>
          <nm-button>默认按钮</nm-button>
          <nm-button type="primary" icon="add">主要按钮</nm-button>
          <nm-button type="success" icon="edit">成功按钮</nm-button>
          <nm-button type="info">信息按钮</nm-button>
          <nm-button type="warning">警告按钮</nm-button>
          <nm-button type="danger">危险按钮</nm-button>
        </el-row>
        <el-row style="margin-top:50px">
          <nm-button icon="search" circle/>
          <nm-button type="primary" icon="add" circle/>
          <nm-button type="success" icon="edit" circle/>
          <nm-button type="warning" icon="edit" circle/>
          <nm-button type="danger" icon="delete" circle/>
        </el-row>
      </nm-demo>
    </nm-box>
  </nm-container>
</template>
<script>
export default {
  data () {
    return {
      demo1: {
        title: '普通按钮',
        height: 300,
        html: `<template>
      <el-row>
        <nm-button>默认按钮</nm-button>
        <nm-button type="primary" icon="add">主要按钮</nm-button>
        <nm-button type="success" icon="edit">成功按钮</nm-button>
        <nm-button type="info">信息按钮</nm-button>
        <nm-button type="warning">警告按钮</nm-button>
        <nm-button type="danger">危险按钮</nm-button>
      </el-row>
      <el-row style="margin-top:50px">
        <nm-button icon="search" circle/>
        <nm-button type="primary" icon="add" circle/>
        <nm-button type="success" icon="edit" circle/>
        <nm-button type="warning" icon="edit" circle/>
        <nm-button type="danger" icon="delete" circle/>
      </el-row>
</template>`
      }
    }
  }
}
</script>
