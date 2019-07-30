export default [
  {
    name: 'model',
    desc: '表单对象，必须',
    type: 'Object',
    opt: '-',
    def: '-'
  },
  {
    name: 'rules',
    desc: '验证规则',
    type: 'Object',
    opt: '-',
    def: '-'
  },
  {
    name: 'action',
    desc: '提交请求，必须',
    type: 'Function',
    opt: '-',
    def: '-'
  },
  {
    name: 'inline',
    desc: '行内表单模式',
    type: 'Boolean',
    opt: '-',
    def: 'false'
  },
  {
    name: 'success-msg',
    desc: '是否显示成功提示消息',
    type: 'Boolean',
    opt: '-',
    def: 'true'
  },
  {
    name: 'success-msg-text',
    desc: '成功提示消息文本',
    type: 'String',
    opt: '-',
    def: '保存成功'
  },
  {
    name: 'label-width',
    desc: '标签的宽度',
    type: 'String',
    opt: '-',
    def: '100px'
  },
  {
    name: 'label-position',
    desc:
      '表单域标签的位置，如果值为 left 或者 right 时，则需要设置 label-width',
    type: 'String',
    opt: 'right/left/top',
    def: 'right'
  },
  {
    name: 'validate',
    desc: '自定义验证，在表单验证通过之后，表单提交之前调用',
    type: 'Function',
    opt: '-',
    def: '-'
  },
  {
    name: 'disabled',
    desc: '禁用表单',
    type: 'Boolean',
    opt: '-',
    def: 'false'
  },
  {
    name: 'loading',
    desc: '显示加载动画',
    type: 'Boolean',
    opt: '-',
    def: 'false'
  },
  {
    name: 'noLoading',
    desc: '不显示加载动画',
    type: 'Boolean',
    opt: '-',
    def: 'false'
  }
]
