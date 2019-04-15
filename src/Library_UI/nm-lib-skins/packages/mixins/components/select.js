export default {
  data() {
    return {
      value_: this.value,
      options: [],
      action: null,
      loading: false
    }
  },
  props: {
    value: {
      type: [String, Number, Array],
      default: ''
    },
    // 多选
    multiple: Boolean,
    // 可清空
    clearable: Boolean,
    // 禁用
    disabled: Boolean,
    // 过滤
    filterable: Boolean,
    // 显示刷新按钮
    showRefresh: Boolean
  },
  computed: {
    selection() {
      let list
      if (!this.value_) return list

      if (this.multiple) {
        list = []
        this.value_.forEach(item => {
          for (var i = 0; i < this.options.length; i++) {
            const opt = this.options[i]
            if (opt.value === item) {
              list.push(opt)
              break
            }
          }
        })
      } else {
        for (var i = 0; i < this.options.length; i++) {
          const opt = this.options[i]
          if (opt.value === this.value_) {
            list = opt
            break
          }
        }
      }
      return list
    }
  },
  mounted() {
    this.refresh()
  },
  methods: {
    change(val) {
      this.value_ = val
    },
    // 刷新
    refresh() {
      this.loading = true
      this.action().then(options => {
        this.options = options
        this.loading = false
      })
    }
  },
  watch: {
    value() {
      this.value_ = this.value
    },
    value_() {
      this.$emit('input', this.value_)
      this.$emit('change', this.value_, this.selection, this.options)
    }
  },
  render(h) {
    return (
      <div
        class="nm-select"
        v-loading={this.loading}
        element-loading-background="rgba(255, 255, 255, 0.5)"
      >
        <div class="nm-select-input">
          <el-select
            placeholder="请选择..."
            value={this.value_}
            clearable={this.clearable}
            multiple={this.multiple}
            disabled={this.disabled}
            size={this.fontSize}
            filterable={this.filterable}
            vOn:change={this.change}
          >
            {this.$scopedSlots.default
              ? this.$scopedSlots.default({ options: this.options })
              : this.options.map(item => {
                  return (
                    <el-option
                      label={item.label}
                      value={item.value}
                      disabled={item.disabled}
                    />
                  )
                })}
          </el-select>
        </div>
        <div class="nm-select-button">
          <nm-button
            v-show={this.showRefresh}
            icon="refresh"
            class="nm-select-botton-refresh"
            vOn:click={this.refresh}
          />
        </div>
      </div>
    )
  }
}
