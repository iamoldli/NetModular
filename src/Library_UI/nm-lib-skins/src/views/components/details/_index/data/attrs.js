export default [
  {
    name: 'action',
    desc: '数据查询方法',
    type: 'Function',
    opt: '-',
    def: '-'
  },
  {
    name: 'model',
    desc: '模型数据，对于不需要通过接口返回的数据，可以通过该属性传递',
    type: 'Object',
    opt: '-',
    def: '-'
  },
  {
    name: 'options',
    desc: '模型配置项',
    type: 'Arrary',
    opt: '-',
    def: '-'
  },
  {
    name: 'label-width',
    desc: '标签的宽度',
    type: 'string',
    opt: '',
    def: '80px'
  },
  {
    name: 'no-loading',
    desc: '不显示加载动画',
    type: 'boolean',
    opt: '-',
    def: 'false'
  },
  {
    name: 'no-border',
    desc: '不显示外边框',
    type: 'boolean',
    opt: '-',
    def: 'false'
  },
  {
    name: 'query-on-created',
    desc: '创建时执行一次查询',
    type: 'boolean',
    opt: '-',
    def: 'true'
  }
]
