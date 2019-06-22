export default {
  data() {
    return {
      value_: this.value,
      options: [],
      action: null,
      loading: false,
      hasInit: false
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
    // 是否可搜索
    filterable: Boolean,
    // 显示刷新按钮
    showRefresh: Boolean,
    /** 多选时用户最多可以选择的项目数，为 0 则不限制 */
    multipleLimit: {
      type: Number,
      default: 0
    },
    /** 占位符 */
    placeholder: {
      type: String,
      default: '请选择'
    },
    /** 是否默认选中第一个 */
    checkedFirst: Boolean
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
    // 刷新
    refresh() {
      this.loading = true
      this.action().then(options => {
        this.options = options
        this.loading = false

        if (this.checkedFirst && !this.hasInit && options.length > 0) {
          this.value_ = options[0].value
          this.hasInit = true
        }
      })
    },
    onChange(val) {
      this.value_ = val
    },
    onVisibleChange(val) {
      this.$emit('visible-change', val)
    },
    onRemoveTag(tag) {
      this.$emit('remove-tag', tag)
    },
    onClear() {
      this.$emit('clear')
    },
    onBlur(event) {
      this.$emit('blur', event)
    },
    onFocus(event) {
      this.$emit('focus', event)
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
            multipleLimit={this.multipleLimit}
            placeholder={this.placeholder}
            vOn:change={this.onChange}
            vOn:visible-change={this.onVisibleChange}
            vOn:remove-tag={this.onRemoveTag}
            vOn:clear={this.onClear}
            vOn:blur={this.onBlur}
            vOn:focus={this.onFocus}
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
