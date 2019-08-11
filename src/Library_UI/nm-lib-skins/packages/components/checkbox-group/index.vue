<template>
  <nm-box header class="nm-checkbox-group">
    <template v-slot:header>
      <el-checkbox v-model="checkAll" :size="size_" :indeterminate="isIndeterminate" :disabled="disabledCheckAll" @change="onCheckAllChange">全选</el-checkbox>
    </template>
    <el-checkbox-group v-model="value_" :size="size_">
      <el-checkbox v-for="item in options" :key="item.value" :label="item.value" :disabled="item.disabled" :border="border">
        <slot :option="item">{{ item.label }}</slot>
      </el-checkbox>
    </el-checkbox-group>
  </nm-box>
</template>
<script>
export default {
  name: 'CheckboxGroup',
  data() {
    return {
      value_: this.value,
      checkAll: false,
      isIndeterminate: true,
      options: []
    }
  },
  props: {
    value: {
      type: Array,
      default() {
        return []
      }
    },
    size: String,
    disabled: Boolean,
    min: Number,
    max: Number,
    border: {
      type: Boolean,
      default: true
    },
    action: Function
  },
  computed: {
    disabledCheckAll() {
      return !this.options || this.options.length < 1 || !this.options.every(o => !o.disabled)
    },
    selection() {
      let list = []
      if (this.value_) {
        this.value_.forEach(item => {
          for (var i = 0; i < this.options.length; i++) {
            const opt = this.options[i]
            if (opt.value === item) {
              list.push(opt)
              break
            }
          }
        })
      }

      return list
    },
    size_() {
      return this.size || this.fontSize
    }
  },
  methods: {
    refresh() {
      this.action().then(options => {
        this.options = options
        this.setCheckAll()
      })
    },
    // 设置全选状态
    setCheckAll() {
      if (this.value_) {
        let selectionCount = this.value_.length
        this.checkAll = selectionCount === this.options.length
        this.isIndeterminate = selectionCount > 0 && selectionCount < this.options.length
      } else {
        this.isIndeterminate = false
      }
    },
    // 处理全选按钮事件
    onCheckAllChange(val) {
      this.value_ = val ? this.options.map(m => m.value) : []
      this.isIndeterminate = false
      this.onChange()
    },
    // 清楚已选项
    clear() {
      this.value_ = this.value
      this.onChange()
    },
    onChange() {
      this.$emit('input', this.value_)
      this.$emit('change', this.value_, this.selection, this.options)
    }
  },
  created() {
    this.refresh()
  },
  watch: {
    value(val) {
      this.value_ = this.value
    }
  }
}
</script>
