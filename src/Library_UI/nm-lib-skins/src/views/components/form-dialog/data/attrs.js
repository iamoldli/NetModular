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
    def: ''
  },
  {
    name: 'width',
    desc: '宽度',
    type: 'String',
    opt: '-',
    def: '50%'
  },
  {
    name: 'fullscreen',
    desc: '是否显示全屏按钮',
    type: 'Boolean',
    opt: '-',
    def: 'false'
  },
  {
    name: 'model',
    desc: '表单模型，必须',
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
    name: 'action',
    desc: '表单提交请求',
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
    name: 'validate',
    desc: '自定义验证方法',
    type: 'Funct',
    opt: '-',
    def: ''
  },
  {
    name: 'btn-ok',
    desc: '是否显示确认按钮',
    type: 'Boolean',
    opt: '-',
    def: 'true'
  },
  {
    name: 'btn-ok-text',
    desc: '确认按钮的显示文本',
    type: 'String',
    opt: '-',
    def: '保存'
  },
  {
    name: 'btn-ok-type',
    desc: '确认按钮的类型',
    type: 'Boolean',
    opt: 'info/success/primary/danger/warning',
    def: 'primary'
  },
  {
    name: 'btn-reset',
    desc: '是否显示重置按钮',
    type: 'Boolean',
    opt: '-',
    def: 'true'
  },
  {
    name: 'close-when-success',
    desc: '保存成功后是否关闭对话框',
    type: 'Boolean',
    opt: '-',
    def: '-'
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
  }
]
