export default [
  {
    name: 'title',
    desc: '标题',
    type: 'string',
    opt: '-',
    def: '-'
  },
  {
    name: 'icon',
    desc: '头部左侧的图标',
    type: 'string',
    opt: '-',
    def: '-'
  },
  {
    name: 'action',
    desc: '数据查询方法，必须',
    type: 'Function',
    opt: '-',
    def: '-'
  },
  {
    name: 'inputWidth',
    desc: '查询表单输入框宽度',
    type: 'String',
    opt: '-',
    def: '-'
  },
  {
    name: 'model',
    desc: '查询表单模型',
    type: 'Object',
    opt: '-',
    def: '-'
  },
  {
    name: 'rules',
    desc: '查询表单模型验证规则',
    type: 'Object',
    opt: '-',
    def: '-'
  },
  {
    name: 'advanced',
    desc: '高级查询',
    type: 'Object',
    opt: '-',
    def: '-'
  },
  {
    name: 'cols',
    desc: '列信息',
    type: 'Arrary',
    opt: '-',
    def: '-'
  },
  {
    name: 'multiple',
    desc: '是否可多选',
    type: 'Boolean',
    opt: '-',
    def: 'false'
  },
  {
    name: 'show-no',
    desc: '是否显示序号',
    type: 'Boolean',
    opt: '-',
    def: 'true'
  },
  {
    name: 'no-operation',
    desc: '不显示操作列',
    type: 'Boolean',
    opt: '-',
    def: 'false'
  },
  {
    name: 'operation-width',
    desc: '操作列宽度',
    type: 'String',
    opt: '-',
    def: '-'
  },
  {
    name: 'no-select-column',
    desc: '不显示选择列按钮',
    type: 'Boolean',
    opt: '-',
    def: 'false'
  },
  {
    name: 'no-querybar',
    desc: '不显示查询栏',
    type: 'Boolean',
    opt: '-',
    def: 'false'
  },
  {
    name: 'no-fullscreen',
    desc: '不显示全屏按钮',
    type: 'Boolean',
    opt: '-',
    def: 'false'
  },
  {
    name: 'no-refresh',
    desc: '不显示刷新按钮',
    type: 'Boolean',
    opt: '-',
    def: 'false'
  },
  {
    name: 'no-header',
    desc: '不显示头部',
    type: 'Boolean',
    opt: '-',
    def: 'false'
  },
  {
    name: 'no-footer',
    desc: '不显示底部',
    type: 'Boolean',
    opt: '-',
    def: 'false'
  },
  {
    name: 'no-search',
    desc: '不包含搜索功能',
    type: 'Boolean',
    opt: '-',
    def: 'false'
  },
  {
    name: 'no-search-button-icon',
    desc: '不显示查询按钮图标',
    type: 'Boolean',
    opt: '-',
    def: 'false'
  },
  {
    name: 'footer-reverse',
    desc: '底部反转',
    type: 'Boolean',
    opt: '-',
    def: 'false'
  },
  {
    name: 'span-method',
    desc: '合并行列的方法',
    type: 'Function',
    opt: '-',
    def: '-'
  },
  {
    name: 'loading',
    desc: '加载中动画',
    type: 'Boolean',
    opt: '-',
    def: 'false'
  },
  {
    name: 'loading-text',
    desc: '加载中文本',
    type: 'String',
    opt: '-',
    def: 'false'
  },
  {
    name: 'query-on-created',
    desc: '创建后执行一次查询',
    type: 'Boolean',
    opt: '-',
    def: 'true'
  }
]
