<template>
  <el-col v-if="show" :span="span">
    <el-select v-model="value_.code" :size="fontSize" :filterable="filterable" :disabled="disabled" @change="onChange">
      <el-option v-for="item in list" :key="item.value" :label="item.label" :value="item.value"></el-option>
    </el-select>
  </el-col>
</template>
<script>
import api from '../../../../api/Area'

export default {
  data() {
    return {
      value_: this.value,
      list: []
    }
  },
  props: {
    value: Object,
    level: Number,
    parentCode: String,
    show: Boolean,
    filterable: Boolean,
    disabled: Boolean,
    span: Number
  },
  methods: {
    async query() {
      this.list = []

      if (this.parentCode) {
        let data = await api.queryChildren(this.parentCode)
        this.list = data.map(item => {
          return {
            label: item.name,
            value: item.code
          }
        })

        // 重新查询数据后，设置下当前选中地区的名称
        this.setName()
      }
    },
    onChange(code) {
      const option = this.list.find(m => m.value === code)
      if (option) {
        this.value_ = { name: option.label, code: option.value }
      } else {
        this.value_ = { name: '', code: '' }
      }

      this.$emit('input', this.value_)

      if (this.level === 4) {
        this.$parent.$parent.onChange()
      } else {
        this.$parent.$parent.resetChild(this.level + 1)
      }
    },
    reset_() {
      this.value_ = { name: '', code: '' }
      this.onChange()
    },
    // 设置名称
    setName() {
      if (this.value_.code) {
        const option = this.list.find(m => m.value === this.value_.code)
        if (option) {
          this.value_.name = option.label
        }
      }
    }
  },
  watch: {
    'value.code'(val) {
      this.value_.code = val
      this.setName()
    },
    parentCode() {
      this.query()
    }
  },
  mounted() {
    this.query()
  }
}
</script>
