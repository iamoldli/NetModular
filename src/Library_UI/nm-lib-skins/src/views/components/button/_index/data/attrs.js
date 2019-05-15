export default [
  {
    name: 'icon',
    desc: '图标，只能使用图标库中已有的图标',
    type: 'string',
    opt: '-',
    def: '-'
  },
  {
    name: 'text',
    desc: '按钮文本',
    type: 'string',
    opt: '-',
    def: '-'
  },
  {
    name: 'size',
    desc: '尺寸，默认或者为空时，按照皮肤的字号设置',
    type: 'string',
    opt: 'medium / small / mini',
    def: '-'
  },
  {
    name: 'type',
    desc: '类型，与ElementUI相同',
    type: 'string',
    opt: 'primary/success/warning/danger/info/text',
    def: '-'
  },
  {
    name: 'plain',
    desc: '是否朴素按钮',
    type: 'boolean',
    opt: '-',
    def: 'false'
  },
  {
    name: 'round',
    desc: '是否圆角按钮',
    type: 'boolean',
    opt: '-',
    def: 'false'
  },
  {
    name: 'circle',
    desc: '是否圆形按钮',
    type: 'boolean',
    opt: '-',
    def: 'false'
  },
  {
    name: 'loading',
    desc: '是否加载中状态',
    type: 'boolean',
    opt: '-',
    def: 'false'
  },
  {
    name: 'disabled',
    desc: '是否禁用状态',
    type: 'boolean',
    opt: '-',
    def: 'false'
  },
  {
    name: 'autofocus',
    desc: '是否默认聚焦',
    type: 'boolean',
    opt: '-',
    def: 'false'
  },
  {
    name: 'nativeType',
    desc: '原生 type 属性 button/submit/reset',
    type: 'string',
    opt: '-',
    def: '-'
  }
]
