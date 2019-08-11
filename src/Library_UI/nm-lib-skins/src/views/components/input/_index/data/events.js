export default [
  {
    name: 'change',
    desc: '选中值发生变化时触发',
    params: '目前的选中值'
  },
  {
    name: 'visible-change',
    desc: '下拉框出现/隐藏时触发',
    params: '出现则为 true，隐藏则为 false'
  },
  {
    name: 'remove-tag',
    desc: '多选模式下移除tag时触发',
    params: '移除的tag值'
  },
  {
    name: 'clear',
    desc: '可清空的单选模式下用户点击清空按钮时触发',
    params: ''
  },
  {
    name: 'blur',
    desc: '当 input 失去焦点时触发',
    params: 'event'
  },
  {
    name: 'focus',
    desc: '当 input 获得焦点时触发',
    params: 'event'
  }
]
