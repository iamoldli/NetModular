<template>
  <nm-container>
    <nm-box page header title="盒子(nm-box)" icon="app">
      <nm-demo-desc>
        <p>盒子，顾名思义就是装东西用的~</p>
      </nm-demo-desc>

      <nm-table-attributes :data="attrs"/>
      <nm-table-slots :data="slots"/>
      <nm-table-methods :data="methods"/>

      <nm-demo :options="demo1">
        <demo1/>
      </nm-demo>
      <nm-demo :options="demo2">
        <demo2/>
      </nm-demo>
      <nm-demo :options="demo4">
        <demo4/>
      </nm-demo>
    </nm-box>
  </nm-container>
</template>
<script>
import attrs from './data/attrs.js'
import slots from './data/slots.js'
import methods from './data/methods.js'
import demo1 from './demo/1'
import demo2 from './demo/2'
import demo4 from './demo/4'
export default {
  components: { demo1, demo2, demo4 },
  data () {
    return {
      attrs,
      slots,
      methods,
      demo1: {
        title: '简单盒子',
        height: 40,
        html: '<nm-box header title="我是个简单的盒子" icon="list">我是个简单的盒子</nm-box>'
      },
      demo2: {
        title: '有头有尾',
        height: 240,
        html: `<template>
  <nm-box header footer title="我是个有头有尾的盒子" icon="list">
    <template v-slot:footer>
      <nm-button type="success">确定</nm-button>
    </template>
  </nm-box>
</template>

` },
      demo4: { title: '工具栏',
        height: 200,
        html: `<template>
  <nm-box header title="我的右边有工具栏~" icon="list" :toolbar="['collapse','fullscreen']">
    <nm-demo-desc>
      <p>通过设置
        <code>header</code>以及
        <code>toolbar</code>属性可以启用工具栏功能，默认工具栏包含折叠和全屏功能
      </p>
    </nm-demo-desc>
  </nm-box>
</template>

`
      },
      selection: {},
      select: []
    }
  },
  methods: {
    query () {
      return new Promise(resolve => {
        resolve([{ label: '选项1', value: '1' }, { label: '选项2', value: '2' }])
      })
    }
  },
  watch: {
    selection () {
      console.log(this.selection)
    }
  }
}
</script>
