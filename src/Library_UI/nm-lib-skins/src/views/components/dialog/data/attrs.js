export default [
  {
    name: 'visible',
    desc: '显示',
    type: 'Boolean',
    opt: '-',
    def: 'false'
  },
  {
    name: 'title',
    desc: '对话框的标题，也可通过具名 title传入',
    type: 'String',
    opt: '-',
    def: ''
  },
  {
    name: 'icon',
    desc: '头部左侧的图标',
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
    name: 'modal',
    desc: '是否需要遮罩层',
    type: 'Boolean',
    opt: '-',
    def: 'true'
  },
  {
    name: 'close-on-click-modal',
    desc: '是否可以通过点击 modal 关闭 Dialog',
    type: 'Boolean',
    opt: '-',
    def: 'true'
  },
  {
    name: 'show-close',
    desc: '是否显示关闭按钮',
    type: 'Boolean',
    opt: '-',
    def: 'true'
  },
  {
    name: 'before-close',
    desc: '关闭前的回调，会暂停 Dialog 的关闭',
    type: 'Function',
    opt: '-',
    def: ''
  },
  {
    name: 'fullscreen',
    desc: '是否显示全屏按钮',
    type: 'Boolean',
    opt: '-',
    def: 'false'
  }
]
