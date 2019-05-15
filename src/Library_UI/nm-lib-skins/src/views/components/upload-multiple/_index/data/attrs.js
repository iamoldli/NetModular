export default [
  {
    name: 'v-model/value',
    desc: '绑定值',
    type: 'String',
    opt: '-',
    def: ''
  },
  {
    name: 'action',
    desc: '上传地址',
    type: 'String',
    opt: '-',
    def: '-'
  },
  {
    name: 'multiple',
    desc:
      '是否启用多选，该属性是控制打开系统的文件选择对话框时，是否可以选中多个，不是上传多个文件，上传单个或多个文件使用limit属性',
    type: 'Boolean',
    opt: '-',
    def: 'true'
  },
  {
    name: 'data',
    desc: '上传时的额外数据',
    type: 'Object',
    opt: '-',
    def: '-'
  },
  {
    name: 'limit',
    desc: '最大允许上传个数',
    type: 'Number',
    opt: '-',
    def: '-'
  },
  {
    name: 'drag',
    desc: '是否可拖拽上传',
    type: 'Boolean',
    opt: '-',
    def: 'true'
  },
  {
    name: 'tip',
    desc: '提示信息',
    type: 'String',
    opt: '-',
    def: '-'
  },
  {
    name: 'always-show-table',
    desc: '始终显示文件列表',
    type: 'Boolean',
    opt: '-',
    def: 'false'
  },
  {
    name: 'max-size',
    desc: '文件最大大小',
    type: 'String',
    opt: '-',
    def: '-'
  },
  {
    name: 'accept',
    desc: '接受上传的文件类型',
    type: 'String',
    opt: '-',
    def: '-'
  }
]
