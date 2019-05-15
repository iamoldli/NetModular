<template>
  <nm-tmpl v-bind="tmpl">
    <template v-slot:after-events>
      <el-alert title="注意事项" type="warning" :closable="false">
        <h1>高级查询属性</h1>
        <pre>
          {
            // 是否开启
            enabled: false,

            // 宽度
            width: '400px',

            // 高度
            height: '',

            // 表单标签宽度
            labelWidth: '100px',

            // 内联表单
            inline: false
          }
        </pre>
        <h1>列信息属性</h1>
        <pre>
          {
            // 列的字段名称
            name: '',

            // 列的显示名称
            label: '',

            // 宽度
            width: '',

            // 排序
            sortable: false,

            // 固定列
            fixed: false,

            // 对其方式
            align: 'center',

            // 表头对其方式
            headerAlign: 'center',

            // 是否显示
            show: true
          }
        </pre>
      </el-alert>
    </template>
  </nm-tmpl>
</template>
<script>
import page from './page.js'
import attrs from './data/attrs.js'
import slots from './data/slots.js'
import methods from './data/methods.js'
import events from './data/events.js'
export default {
  name: page.name,
  data() {
    return {
      val: '',
      tmpl: {
        title: '列表页(nm-list)',
        icon: 'card',
        attrs,
        slots,
        methods,
        events
      }
    }
  },
  created() {
    this.setTabName(this.tmpl.title)
  }
}
</script>
