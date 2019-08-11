export default [
  {
    name: 'v-model/value',
    desc: '绑定值',
    type: 'String, Number, Array',
    opt: '-',
    def: '-'
  },
  {
    name: 'method',
    desc: '数据请求方法，必须',
    type: 'Function',
    opt: '-',
    def: '-'
  },
  {
    name: 'multiple',
    desc: '多选',
    type: 'Boolean',
    opt: '-',
    def: 'false'
  },
  {
    name: 'clearable',
    desc: '可清空',
    type: 'Boolean',
    opt: '-',
    def: 'true'
  },
  {
    name: 'disabled',
    desc: '禁用',
    type: 'Boolean',
    opt: '-',
    def: 'false'
  },
  {
    name: 'filterable',
    desc: '是否可搜索',
    type: 'Boolean',
    opt: '-',
    def: 'false'
  },
  {
    name: 'show-refresh',
    desc: '显示刷新按钮',
    type: 'Boolean',
    opt: '-',
    def: 'false'
  },
  {
    name: 'multiple-limit',
    desc: '多选时用户最多可以选择的项目数，为 0 则不限制',
    type: 'Number',
    opt: '-',
    def: '0'
  },
  {
    name: 'placeholder',
    desc: '占位符',
    type: 'String',
    opt: '-',
    def: '请选择'
  }
]
