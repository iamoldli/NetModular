export default [
  {
    name: 'title',
    desc: '标题',
    type: 'String',
    opt: '-',
    def: ''
  },
  {
    name: 'icon',
    desc: '图标',
    type: 'String',
    opt: '-',
    def: 'detail'
  },
  {
    name: 'type',
    desc: '类型，盒子顶部边框颜色，为空表示没有',
    type: 'string',
    opt: 'info、primary、success、warning、danger',
    def: 'success'
  },
  {
    name: 'action',
    desc: '查询列表的方法',
    type: 'Function',
    opt: '-',
    def: ''
  },
  {
    name: 'conditions',
    desc: '过滤条件对象',
    type: 'Object',
    opt: '-',
    def: ''
  },
  {
    name: 'cols',
    desc: '要显示的列数组',
    type: 'Array',
    opt: '-',
    def: ''
  },
  {
    name: 'tool-select-col',
    desc: '是否显示选择列按钮',
    type: 'Boolean',
    opt: '-',
    def: 'true'
  },
  {
    name: 'span-method',
    desc: '合并行或列的计算方法,参考elemnetui的用法',
    type: 'Function({ row, column, rowIndex, columnIndex })',
    opt: '-',
    def: ''
  },
  {
    name: 'inline',
    desc: '行内表单',
    type: 'Boolean',
    opt: '-',
    def: 'true'
  }
]
