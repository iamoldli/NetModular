<template>
  <section class="nm-list-body-table">
    <el-table ref="table" v-bind="table" v-on="on">
      <slot/>
    </el-table>
  </section>
</template>
<script>
export default {
  data () {
    return {
      on: {
        'select': this.onSelect,
        'select-all': this.onSelectAll,
        'selection-change': this.onSelectionChange,
        'cell-mouse-enter': this.onCellMouseEnter,
        'cell-mouse-leave': this.onCellMouseLeave,
        'cell-click': this.onCellClick,
        'cell-dblclick': this.onCellDblclick,
        'row-click': this.onRowClick,
        'row-contextmenu': this.onRowContextmenu,
        'row-dblclick': this.onRowDblclick,
        'header-click': this.onHeaderClick,
        'header-contextmenu': this.onHeaderContextmenu,
        'sort-change': this.onSortChange,
        'current-change': this.onCurrentChange
      }
    }
  },
  props: {
    /** 选择的列 */
    selection: Array,
    /** 高度 */
    height: {
      type: String,
      default: '100%'
    },
    /** 列集合 */
    cols: {
      type: Array,
      required: true
    },
    /** 数据 */
    rows: {
      type: Array,
      default () {
        return []
      }
    },
    /** 合并行列方法 */
    spanMethod: Function
  },
  computed: {
    table () {
      return {
        border: true,
        stripe: true,
        highlightCurrentRow: true,
        size: this.fontSize,
        height: this.height,
        spanMethod: this.spanMethod,
        headerRowClassName: 'nm-list-body-table-header',
        data: this.rows
      }
    }
  },
  methods: {
    /** 清除排序 */
    clearSort () {
      this.$refs.table.clearSort()
    },
    /** 滚动到顶部 */
    scrollTop () {
      this.$nextTick(() => {
        this.$el.querySelector('.el-table__body-wrapper').scrollTop = 0
      })
    },
    /** 当用户手动勾选数据行的 Checkbox 时触发的事件 */
    onSelect (selection, row) {
      this.$parent.$emit('select', selection, row)
    },
    /** 当用户手动勾选全选 Checkbox 时触发的事件 */
    onSelectAll (selection) {
      this.$parent.$emit('select-all', selection)
    },
    /** 当选择项发生变化时会触发该事件 */
    onSelectionChange (selection) {
      this.$emit('update:selection', selection)
      this.$parent.$emit('selection-change', selection)
    },
    /** 当单元格 hover 进入时会触发该事件 */
    onCellMouseEnter (row, column, cell, event) {
      this.$parent.$emit('cell-mouse-enter', row, column, cell, event)
    },
    /** 当单元格 hover 退出时会触发该事件 */
    onCellMouseLeave (row, column, cell, event) {
      this.$parent.$emit('cell-mouse-leave', row, column, cell, event)
    },
    /** 当某个单元格被点击时会触发该事件 */
    onCellClick (row, column, cell, event) {
      this.$parent.$emit('cell-click', row, column, cell, event)
    },
    /** 当某个单元格被双击击时会触发该事件 */
    onCellDblclick (row, column, cell, event) {
      this.$parent.$emit('cell-dblclick', row, column, cell, event)
    },
    /** 当某一行被点击时会触发该事件 */
    onRowClick (row, event, column) {
      this.$parent.$emit('row-click', row, event, column)
    },
    /** 当某一行被鼠标右键点击时会触发该事件 */
    onRowContextmenu (row, event) {
      this.$parent.$emit('row-contextmenu', row, event)
    },
    /** 当某一行被双击时会触发该事件 */
    onRowDblclick (row, event) {
      this.$parent.$emit('row-dblclick', row, event)
    },
    /** 当某一列的表头被点击时会触发该事件 */
    onHeaderClick (column, event) {
      this.$parent.$emit('header-click', column, event)
    },
    /** 当某一列的表头被鼠标右键点击时触发该事件 */
    onHeaderContextmenu (column, event) {
      this.$parent.$emit('header-contextmenu', column, event)
    },
    /** 当表格的排序条件发生变化的时候会触发该事件 */
    onSortChange (val) {
      this.page.sort = []
      // 将排序信息转化成后端的格式
      if (val.prop !== null) {
        this.page.sort.push({ field: val.prop, type: val.order === 'ascending' ? 0 : 1 })
      }

      this.refresh()

      this.$parent.$emit('sort-change', this.page.sort, val)
    },
    /** 当表格的当前行发生变化的时候会触发该事件，如果要高亮当前行，请打开表格的 highlight-current-row 属性 */
    onCurrentChange (currentRow, oldCurrentRow) {
      this.$parent.$emit('current-change', currentRow, oldCurrentRow)
    }
  }
}
</script>
