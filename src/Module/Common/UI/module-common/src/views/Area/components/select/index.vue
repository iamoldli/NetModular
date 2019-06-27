<template>
  <section class="nm-area-select-box">
    <el-row :gutter="gutter">
      <el-col :span="span">
        <el-select v-model="selection.v1" :size="fontSize" :filterable="filterable" @change="onChange(1)">
          <el-option v-for="item in data.d1" :key="item.value" :label="item.label" :value="item.value"></el-option>
        </el-select>
      </el-col>
      <el-col v-if="level>1" :span="span">
        <el-select v-model="selection.v2" :size="fontSize" :filterable="filterable" @change="onChange(2)">
          <el-option v-for="item in data.d2" :key="item.value" :label="item.label" :value="item.value"></el-option>
        </el-select>
      </el-col>
      <el-col v-if="level>2" :span="span">
        <el-select v-model="selection.v3" :size="fontSize" :filterable="filterable" @change="onChange(3)">
          <el-option v-for="item in data.d3" :key="item.value" :label="item.label" :value="item.value"></el-option>
        </el-select>
      </el-col>
      <el-col v-if="level>3" :span="span">
        <el-select v-model="selection.v4" :size="fontSize" :filterable="filterable" @change="onChange(4)">
          <el-option v-for="item in data.d4" :key="item.value" :label="item.label" :value="item.value"></el-option>
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
      selection: {
        v1: '',
        v2: '',
        v3: '',
        v4: ''
      },
      data: {
        d1: [],
        d2: [],
        d3: [],
        d4: []
      },
      // 当前要查询的等级
      queryLevel: 1
    }
  },
  props: {
    value: Array,
    /** 默认值，区域编码列表 */
    defaultValue: Array,
    /**
     * 级别0、1、2、3 分别对应省、市、区县、乡镇
     */
    level: {
      type: Number,
      default: 1
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
    filterable: Boolean
  },
  computed: {
    span() {
      if (this.level === 2) { return 12 }
      if (this.level === 3) { return 8 }
      if (this.level === 4) { return 6 }
      return 24
    }
  },
  methods: {
    async query() {
      let parentId = 0
      if (this.queryLevel > 1) {
        parentId = this.selection['v' + (this.queryLevel - 1)]
      }
      let data = await api.queryChildren(parentId)
      const options = data.map(item => {
        return {
          label: item.name,
          value: item.id,
          code: item.code
        }
      })
      this.data['d' + this.queryLevel] = options
    },
    reset() {
      for (let i = this.queryLevel; i <= this.level; i++) {
        this.selection['v' + i] = ''

        if (this.queryLevel <= i) {
          this.data['d' + i] = []
        }
      }
    },
    onChange(level) {
      if (this.level > level) {
        this.queryLevel = ++level
        this.reset()
        this.query()
      }

      let value = []
      for (let i = 1; i < 5; i++) {
        if (i <= this.level && this.selection['v' + i]) {
          const option = this.data['d' + i].find(item => item.value === this.selection['v' + i])
          value.push(this.option2Value(option))
        }
      }
      this.$emit('input', value)
      this.$emit('change', value)
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
    /**
     * @description 设置默认值
     */
    async setDefaultValue() {
      if (this.defaultValue && this.defaultValue.length > 0) {
        for (let i = 1; i <= Math.min(this.defaultValue.length, this.level); i++) {
          const code = this.defaultValue[i - 1]
          const option = this.data['d' + i].find(item => item.code === code)
          if (option) {
            this.selection['v' + i] = option.value
            if (this.level > i) {
              this.queryLevel = i + 1
              this.reset()
              await this.query()
            }
          } else {
            break
          }
        }
      } else {
        for (let i = 1; i <= this.level; i++) {
          this.selection['v' + i] = ''
          if (this.queryLevel < i) {
            this.data['d' + i] = []
          }
        }
      }
    }
  },
  mounted() {
    this.query().then(() => {
      this.setDefaultValue()
    })
  },
  watch: {
    defaultValue(val) {
      this.setDefaultValue()
    }
  }
}
</script>
<style lang="scss">
.nm-area-select-box {
  overflow: hidden;
}
</style>
