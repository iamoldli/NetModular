<template>
  <footer class="nm-list-footer" :class="[reverse?'reverse':'']">
    <div class="nm-list-footer-left">
      <slot/>
    </div>
    <div class="nm-list-footer-right">
      <el-pagination
        background
        :current-page="value.index"
        :page-size="value.size"
        :total="total"
        :page-sizes="[10, 15, 50, 100]"
        layout="total, sizes, prev, pager, next, jumper"
        @size-change="onSizeChange"
        @current-change="onIndexChange"
      />
      <select-column v-if="!noSelectColumn" :columns="columns" @change="onSelectColumnChange"/>
    </div>
  </footer>
</template>
<script>
import SelectColumn from '../select-column'
export default {
  components: { SelectColumn },
  props: {
    /** 分页信息 */
    value: {
      type: Object,
      required: true
    },
    /** 总数 */
    total: Number,
    /** 列集合 */
    columns: Array,
    /** 不显示选择列按钮 */
    noSelectColumn: Boolean,
    /** 左右反转 */
    reverse: Boolean
  },
  methods: {
    onSizeChange(size) {
      const page = Object.assign({}, this.value, { size })
      this.$emit('input', page)
      this.$parent.query()
      this.$parent.$emit('size-change', size)
    },
    onIndexChange(index) {
      const page = Object.assign({}, this.value, { index })
      this.$emit('input', page)
      this.$parent.query()
      this.$parent.$emit('index-change', index)
    },
    onSelectColumnChange(columns) {
      this.$emit('update:columns', columns)
    }
  }
}
</script>
