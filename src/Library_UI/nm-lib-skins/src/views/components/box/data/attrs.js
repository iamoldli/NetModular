export default [
  {
    name: 'title',
    desc: '标题，只有设置header属性为时才有',
    type: 'string',
    opt: '-',
    def: '-'
  },
  {
    name: 'icon',
    desc: '头部左侧的图标，只有设置header属性为时才有',
    type: 'string',
    opt: '-',
    def: '-'
  },
  {
    name: 'type',
    desc: '类型，盒子顶部边框颜色，为空表示没有',
    type: 'string',
    opt: 'info、primary、success、warning、danger',
    def: 'success'
  },
  {
    name: 'header',
    desc: '是否显示头部',
    type: 'boolean',
    opt: '-',
    def: 'false'
  },
  {
    name: 'footer',
    desc: '是否显示底部',
    type: 'boolean',
    opt: '-',
    def: 'false'
  },
  {
    name: 'border',
    desc: '是否显示顶部的边框',
    type: 'boolean',
    opt: '-',
    def: 'true'
  },
  {
    name: 'height',
    desc:
      '高度,与css中的height属性相同，如：100%、100px，设置了固定高度后如果内容超出会有滚动条',
    type: 'string',
    opt: '-',
    def: 'false'
  },
  {
    name: 'toolbar',
    desc: '头部右侧的菜单栏',
    type: 'array',
    opt: 'collapse、fullscreen',
    def: '-'
  },
  {
    name: 'custom-collapse-event',
    desc:
      '自定义折叠事件，改参数会覆盖默认的折叠操作，当需要折叠部分内容时，可使用该参数',
    type: 'Function',
    opt: '',
    def: '-'
  },
  {
    name: 'loading',
    desc: '是否显示loading动画',
    type: 'boolean',
    opt: '',
    def: 'false'
  }
]
