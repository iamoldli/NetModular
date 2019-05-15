export default [
  {
    name: 'value',
    desc: '选择的值',
    type: 'Arrary',
    opt: '-',
    def: '-'
  },
  {
    name: 'action',
    desc: '数据项查询方法',
    type: 'Function',
    opt: '-',
    def: '-'
  },
  {
    name: 'size',
    desc:
      '多选框组尺寸，仅对按钮形式的 Checkbox 或带有边框的 Checkbox 有效，默认或者为空时，按照皮肤的字号设置',
    type: 'String',
    opt: 'medium / small / mini',
    def: '-'
  },
  {
    name: 'disabled',
    desc: '是否禁用',
    type: 'Boolean',
    opt: '-',
    def: 'false'
  },
  {
    name: 'min',
    desc: '可被勾选的 checkbox 的最小数量',
    type: 'Number',
    opt: '-',
    def: '-'
  },
  {
    name: 'max',
    desc: '可被勾选的 checkbox 的最大数量',
    type: 'Number',
    opt: '-',
    def: '-'
  },
  {
    name: 'border',
    desc: '是否显示边框',
    type: 'Boolean',
    opt: '-',
    def: 'true'
  }
]
