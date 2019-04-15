export default [
  {
    name: 'model',
    desc: '表单对象',
    type: 'Object',
    opt: '-',
    def: ''
  },
  {
    name: 'rules',
    desc: '验证规则',
    type: 'Object',
    opt: '-',
    def: ''
  },
  {
    name: 'inline',
    desc: '行内表单模式',
    type: 'Boolean',
    opt: '-',
    def: 'false'
  },
  {
    name: 'action',
    desc: '提交请求',
    type: 'Function',
    opt: '-',
    def: ''
  },
  {
    name: 'label-width',
    desc: '标签宽度',
    type: 'String',
    opt: '-',
    def: '120px'
  },
  {
    name: 'success-msg',
    desc: '是否显示成功消息提醒',
    type: 'Boolean',
    opt: '-',
    def: 'true'
  },
  {
    name: 'success-msg-text',
    desc: '成功消息提醒文本',
    type: 'String',
    opt: '-',
    def: '保存成功'
  },
  {
    name: 'validate',
    desc: '自定义验证',
    type: 'Function',
    opt: '-',
    def: ''
  }
]
