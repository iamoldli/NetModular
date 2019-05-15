<template>
  <el-dropdown class="nm-list-select-column" :size="fontSize" trigger="click" :hide-on-click="false" @visible-change="onVisible">
    <span>
      选择列
      <i class="el-icon--right" :class="[visible?'el-icon-arrow-up':'el-icon-arrow-down']"></i>
    </span>
    <el-dropdown-menu slot="dropdown">
      <el-dropdown-item>
        <el-checkbox :indeterminate="isIndeterminate" v-model="checkAll" @change="handleCheckAllChange">全选</el-checkbox>
      </el-dropdown-item>
      <el-checkbox-group v-model="selection" @change="change">
        <el-dropdown-item v-for="col in columns" :key="col.name">
          <el-checkbox :label="col.name" :checked="col.show">{{col.label}}</el-checkbox>
        </el-dropdown-item>
      </el-checkbox-group>
    </el-dropdown-menu>
  </el-dropdown>
</template>
<script>
export default {
  data() {
    return {
      checkAll: false,
      isIndeterminate: true,
      selection: [],
      visible: false,
      columns_: this.columns
    }
  },
  props: ['columns'],
  methods: {
    change() {
      let checkedCount = this.selection.length
      this.checkAll = checkedCount === this.columns_.length
      this.isIndeterminate = checkedCount > 0 && checkedCount < this.columns_.length
      this.handleEvent()
    },
    handleCheckAllChange(val) {
      this.selection = val ? this.columns_.map(item => item.name) : []
      this.isIndeterminate = false
      this.handleEvent()
    },
    handleEvent() {
      this.columns_.map(col => {
        col.show = !this.selection.every(name => name !== col.name)
      })
      this.$emit('change', this.columns_)
    },
    onVisible(visible) {
      this.visible = visible
    }
  }
}
</script>
