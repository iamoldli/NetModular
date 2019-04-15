export default [
  {
    name: 'size-change',
    desc: '分页页大小改变事件',
    params: 'size:更改后的页大小'
  },
  {
    name: 'index-change',
    desc: '分页页码改变事件',
    params: 'index:更改后的页码'
  },
  {
    name: 'select',
    desc: '当用户手动勾选数据行的 Checkbox 时触发的事件',
    params: 'selection, row'
  },
  {
    name: 'select-all',
    desc: '当用户手动勾选全选 Checkbox 时触发的事件',
    params: 'selection'
  },
  {
    name: 'selection-change',
    desc: '当选择项发生变化时会触发该事件',
    params: 'selection'
  },
  {
    name: 'cell-mouse-enter',
    desc: '当单元格 hover 进入时会触发该事件',
    params: 'row, column, cell, event'
  },
  {
    name: 'cell-mouse-leave',
    desc: '当单元格 hover 退出时会触发该事件',
    params: 'row, column, cell, event'
  },
  {
    name: 'cell-click',
    desc: '当某个单元格被点击时会触发该事件',
    params: 'row, column, cell, event'
  },
  {
    name: 'cell-dblclick',
    desc: '当某个单元格被双击击时会触发该事件',
    params: 'row, column, cell, event'
  },
  {
    name: 'row-click',
    desc: '当某一行被点击时会触发该事件',
    params: 'row, event, column'
  },
  {
    name: 'row-contextmenu',
    desc: '当某一行被鼠标右键点击时会触发该事件',
    params: 'row, event'
  },
  {
    name: 'row-dblclick',
    desc: '当某一行被双击时会触发该事件',
    params: 'row, event'
  },
  {
    name: 'header-click',
    desc: '当某一列的表头被点击时会触发该事件',
    params: 'column, event'
  },
  {
    name: 'header-contextmenu',
    desc: '当某一列的表头被鼠标右键点击时触发该事件',
    params: 'column, event'
  },
  {
    name: 'sort-change',
    desc: '当表格的排序条件发生变化的时候会触发该事件',
    params:
      'sorts(排序字段数组，这是自定义的，如果需要elementui的格式，请使用第二个参数), val'
  },
  {
    name: 'current-change',
    desc: '当表格的当前行发生变化的时候会触发该事件',
    params: 'currentRow, oldCurrentRow'
  }
]
