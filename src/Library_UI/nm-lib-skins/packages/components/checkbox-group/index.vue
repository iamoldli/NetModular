<template>
  <nm-box header class="nm-checkbox-group">
    <template v-slot:header>
      <el-checkbox :indeterminate="isIndeterminate" :disabled="disabledCheckAll" v-model="checkAll" @change="onCheckAllChange" :size="fontSize">全选</el-checkbox>
    </template>
    <el-checkbox-group v-model="value_" :size="fontSize">
      <el-checkbox v-for="item in options" :key="item.value" :label="item.value" :disabled="item.disabled" :border="border">
        <slot :option="item">{{ item.label }}</slot>
      </el-checkbox>
    </el-checkbox-group>
  </nm-box>
</template>
<script>
export default {
  name: 'CheckboxGroup',
  data () {
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
      default () {
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
    disabledCheckAll () {
      return !this.options || this.options.length < 1 || !this.options.every(o => !o.disabled)
    },
    selection () {
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
    }
  },
  methods: {
    refresh () {
      this.action().then(options => {
        this.options = options
        this.setCheckAll()
      })
    },
    // 设置全选状态
    setCheckAll () {
      if (this.value_) {
        let selectionCount = this.value_.length
        this.checkAll = selectionCount === this.options.length
        this.isIndeterminate = selectionCount > 0 && selectionCount < this.options.length
      } else {
        this.isIndeterminate = false
      }
    },
    // 处理全选按钮事件
    onCheckAllChange (val) {
      this.value_ = val ? this.options.map(m => m.value) : []
      this.isIndeterminate = false
    },
    // 清楚已选项
    clear () {
      this.value_ = this.value
    }
  },
  created () {
    this.refresh()
  },
  watch: {
    value (val) {
      this.value_ = this.value
    },
    value_ (val) {
      this.setCheckAll()
      this.$emit('input', val)
      this.$emit('change', val, this.selection, this.options)
    }
  }
}
</script>
