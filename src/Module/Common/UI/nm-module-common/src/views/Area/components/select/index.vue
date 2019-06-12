<template>
  <section class="nm-area-select-box">
    <el-row :gutter="gutter">
      <el-col :span="span">
        <el-select v-model="l0" :size="fontSize" :filterable="filterable">
          <el-option v-for="item in data0" :key="item.value" :label="item.label" :value="item.value"></el-option>
        </el-select>
      </el-col>
      <el-col v-if="level>0" :span="span">
        <el-select v-model="l1" :size="fontSize" :filterable="filterable">
          <el-option v-for="item in data1" :key="item.value" :label="item.label" :value="item.value"></el-option>
        </el-select>
      </el-col>
      <el-col v-if="level>1" :span="span">
        <el-select v-model="l2" :size="fontSize" :filterable="filterable">
          <el-option v-for="item in data2" :key="item.value" :label="item.label" :value="item.value"></el-option>
        </el-select>
      </el-col>
      <el-col v-if="level>2" :span="span">
        <el-select v-model="l3" :size="fontSize" :filterable="filterable">
          <el-option v-for="item in data3" :key="item.value" :label="item.label" :value="item.value"></el-option>
        </el-select>
      </el-col>
    </el-row>
  </section>
</template>
<script>
import api from '../../../../api/Area'
export default {
  data() {
    return {
      value_: this.value,
      valueChange: true,
      l0: '',
      l1: '',
      l2: '',
      l3: '',
      data0: [],
      data1: [],
      data2: [],
      data3: []
    }
  },
  computed: {
    span() {
      if (this.level === 1) { return 12 }
      if (this.level === 2) { return 8 }
      if (this.level === 3) { return 6 }
      return 24
    }
  },
  props: {
    value: {
      type: Array
    },
    /**
     * 级别0、1、2、3 分别对应省、市、区县、乡镇
     */
    level: {
      type: Number,
      default: 0
    },
    /**
     * 各个下拉框间隔
     */
    gutter: {
      type: Number,
      default: 10
    },
    /**
     * 返回的数据类型 all、id、name、code
     */
    dataType: {
      type: String,
      default: 'all',
      validator(val) {
        return ['all', 'name', 'code', 'value'].indexOf(val) !== -1
      }
    },
    /** 是否可搜索 */
    filterable: {
      type: Boolean,
      default: false
    }
  },
  methods: {
    async query(level) {
      let data = []
      switch (level) {
        case 0:
          data = await api.queryChildren(0)
          break
        case 1:
          if (this.l0 > 0) {
            data = await api.queryChildren(this.l0)
          }
          break
        case 2:
          if (this.l1 > 0) {
            data = await api.queryChildren(this.l1)
          }
          break
        case 3:
          if (this.l2 > 0) {
            data = await api.queryChildren(this.l2)
          }
          break
      }

      const options = data.map(item => {
        return {
          label: item.name,
          value: item.id,
          code: item.code
        }
      })
      switch (level) {
        case 0:
          this.data0 = options
          break
        case 1:
          this.data1 = options
          break
        case 2:
          this.data2 = options
          break
        case 3:
          this.data3 = options
          break
      }

      this.reset(level)
    },
    reset(level) {
      if (this.valueChange) { return }

      if (level < 2 && this.level > 0) {
        this.l1 = ''
      }
      if (level < 3 && this.level > 1) {
        this.l2 = ''
      }
      if (level < 4 && this.level > 2) {
        this.l3 = ''
      }
    },
    async onChange(level, val) {
      if (this.valueChange) { return }

      if (level < 3) {
        await this.query(level + 1)
      }

      if (level >= 0 && val < 1) {
        return
      }

      let value = []
      if (this.l0 > 0) {
        const n0 = this.data0.find(item => item.value === this.l0)
        value.push(this.option2Value(n0))
      }
      if (this.l1 > 0) {
        const n1 = this.data1.find(item => item.value === this.l1)
        value.push(this.option2Value(n1))
      }
      if (this.l2 > 0) {
        const n2 = this.data2.find(item => item.value === this.l2)
        value.push(this.option2Value(n2))
      }
      if (this.l3 > 0) {
        const n3 = this.data3.find(item => item.value === this.l3)
        value.push(this.option2Value(n3))
      }
      this.value_ = value
      this.$emit('input', this.value_)
      this.$emit('change', this.value_)
    },
    option2Value(option) {
      if (this.dataType === 'all') {
        return {
          id: option.value,
          name: option.label,
          value: option.code
        }
      } else if (this.dataType === 'id') {
        return option.value
      } else if (this.dataType === 'name') {
        return option.label
      } else if (this.dataType === 'code') {
        return option.code
      }
    },
    /** 解析值 */
    async resolveValue() {
      this.valueChange = true
      await this.query(0)

      // datatype=all
      if (this.dataType === 'all') {

      } else {
        await this.resolveValue1(this.dataType)
      }

      // 取消值更改标志
      this.valueChange = false
    },
    /** 解析datatype=code id name的值 */
    async resolveValue1(key) {
      if (this.value && typeof this.value === 'object') {
        if (this.value.length > 0) {
          const v = this.value[0]
          const node = this.data0.find(item => item[key] === v)
          if (node) { this.l0 = node.value }
        }

        if (this.value.length > 1) {
          await this.query(1)
          const v = this.value[1]
          const node = this.data1.find(item => item[key] === v)
          if (node) { this.l1 = node.value }
        }

        if (this.value.length > 2) {
          await this.query(2)
          const v = this.value[2]
          const node = this.data2.find(item => item[key] === v)
          if (node) { this.l2 = node.value }
        }

        if (this.value.length > 3) {
          await this.query(3)
          const v = this.value[3]
          const node = this.data3.find(item => item[key] === v)
          if (node) { this.l3 = node.value }
        }
      }
    }
  },
  mounted() {
    this.resolveValue()
  },
  watch: {
    l0(val) {
      this.onChange(0, val)
    },
    l1(val) {
      this.onChange(1, val)
    },
    l2(val) {
      this.onChange(2, val)
    },
    l3(val) {
      this.onChange(3, val)
    },
    value(val) {
      if (val !== this.value_) {
        this.resolveValue()
      }
    }
  }
}
</script>
<style lang="scss">
.nm-area-select-box {
  overflow: hidden;
}
</style>
