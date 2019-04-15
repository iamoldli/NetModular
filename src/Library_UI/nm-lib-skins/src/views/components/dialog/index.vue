<template>
  <nm-container>
    <nm-box page header title="对话框(nm-dialog)" icon="dialog">
      <nm-demo-desc>
        <p>
          对话框是对
          <code>el-dialog</code>组件的封装，用法基本一致
        </p>
      </nm-demo-desc>

      <nm-table-attributes :data="attrs"/>
      <nm-table-slots :data="slots"/>
      <nm-table-methods :data="methods"/>
      <nm-table-events :data="events"/>

      <nm-demo :options="demo1">
        <demo1/>
      </nm-demo>
      <nm-demo :options="demo2">
        <demo2/>
      </nm-demo>
      <nm-demo :options="demo3">
        <demo3/>
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
import events from './data/events.js'
import demo1 from './demo/1'
import demo2 from './demo/2'
import demo3 from './demo/3'
import demo4 from './demo/4'
export default {
  components: { demo1, demo2, demo3, demo4 },
  data () {
    return {
      attrs,
      slots,
      methods,
      events,
      demo1: {
        title: '简单对话框',
        height: 150,
        html: `  <section>
    <nm-button type="success" @click="visible=true">打开</nm-button>
    <nm-dialog title="简单对话框" icon="list" :visible.sync="visible">
      <h1>哈哈哈哈哈哈哈哈哈</h1>
    </nm-dialog>
  </section>`,
        js: `export default {
  data () {
    return {
      visible: false
    }
  }
}`
      },
      demo2: {
        title: '可全屏的对话框',
        height: 200,
        html: `    <nm-demo-desc>
      <p>
        添加
        <code>fullscreen</code>属性，可以开启全屏功能
      </p>
    </nm-demo-desc>
    <nm-button type="success" @click="visible=true">打开</nm-button>
    <nm-dialog title="可全屏的对话框" :visible.sync="visible" fullscreen>
      <h1>可全屏的对话框</h1>
    </nm-dialog>
  </section>`,
        js: `export default {
  data () {
    return {
      visible: false
    }
  }
}` },
      demo3: {
        title: '自定义底部按钮',
        height: 260,
        html: `  <section>
    <nm-demo-desc>
      <p>通过
        <code>footer</code>插槽，可以在底部添加自定义内容
      </p>
    </nm-demo-desc>
    <nm-button type="success" @click="visible=true">打开</nm-button>
    <nm-dialog ref="myDialog" title="自定义底部按钮" :visible.sync="visible" fullscreen>
      <h1>自定义底部按钮</h1>
      <template v-slot:footer>
        <nm-button type="success">确定</nm-button>
        <nm-button type="info" @click="close">关闭</nm-button>
      </template>
    </nm-dialog>
  </section>`,
        js: `export default {
  data () {
    return {
      visible: false
    }
  },
  methods: {
    close () {
      this.$refs.myDialog.close()
    }
  }
}` },
      demo4: {
        title: '监听事件',
        height: 200,
        html: `  <section>
    <nm-demo-desc>
      <p>监听事件</p>
    </nm-demo-desc>
    <nm-button type="success" @click="visible=true">打开</nm-button>
    <nm-dialog
      ref="myDialog"
      title="监听事件"
      :visible.sync="visible"
      @open="_success('打开了')"
      @close="_warning('关闭了')"
    >
      <h1>监听事件</h1>
    </nm-dialog>
  </section>`,
        js: `export default {
  data () {
    return {
      visible: false
    }
  }
}`
      }
    }
  }
}
</script>
