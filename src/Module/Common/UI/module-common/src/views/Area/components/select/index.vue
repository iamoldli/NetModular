<template>
  <section class="nm-area-select-box">
    <el-row :gutter="gutter">
      <item ref="l1" :level="1" :show="province.show" :disabled="province.disabled" v-model="value_.province" :span="span" parent-code="0" />
      <item ref="l2" :level="2" :show="city.show" :disabled="city.disabled" v-model="value_.city" :span="span" :parent-code="value_.province.code" />
      <item ref="l3" :level="3" :show="area.show" :disabled="area.disabled" v-model="value_.area" :span="span" :parent-code="value_.city.code" />
      <item ref="l4" :level="4" :show="town.show" :disabled="town.disabled" v-model="value_.town" :span="span" :parent-code="value_.area.code" />
    </el-row>
  </section>
</template>
<script>
import { assist } from 'nm-lib-utils'
import Item from './item'
// 默认值
const defaultValue = {
  province: {
    name: '',
    code: ''
  },
  city: {
    name: '',
    code: ''
  },
  area: {
    name: '',
    code: ''
  },
  town: {
    name: '',
    code: ''
  },
  /** 完整名称 */
  fullName: ''
}

export default {
  components: { Item },
  data() {
    return {
      levelList: ['province', 'city', 'area', 'town'],
      value_: Object.assign({}, assist.deepCopy(defaultValue), assist.deepCopy(this.value)),
      initValue: Object.assign({}, assist.deepCopy(defaultValue), assist.deepCopy(this.value))
    }
  },
  props: {
    value: {
      type: Object,
      default() {
        return defaultValue
      }
    },
    level: {
      type: String,
      default: '1234',
      validator(val) {
        return ['1234', '123', '12', '1', '234', '23', '2', '34', '3', '4'].indexOf(val) !== -1
      }
    },
    disabledList: {
      type: String
    },
    /**
     * 下拉框的间隔
     */
    gutter: {
      type: Number,
      default: 10
    },
    /** 是否可搜索 */
    filterable: Boolean,
    /** 完整名称中间的分隔符 */
    separator: {
      type: String,
      default: ''
    }
  },
  computed: {
    province() {
      return this.getOptions('1')
    },
    city() {
      return this.getOptions('2')
    },
    area() {
      return this.getOptions('3')
    },
    town() {
      return this.getOptions('4')
    },
    // 间隔
    span() {
      return 24 / this.level.length
    }
  },
  methods: {
    init() {
      this.value_ = Object.assign({}, assist.deepCopy(defaultValue), assist.deepCopy(this.value))
      this.initValue = Object.assign({}, assist.deepCopy(defaultValue), assist.deepCopy(this.value))
    },
    getOptions(level) {
      return {
        show: this.level.indexOf(level) > -1,
        disabled: this.disabledList ? this.disabledList.indexOf(level) > -1 : false
      }
    },
    reset() {
      // 重置为默认值
      this.value_ = Object.assign({}, assist.deepCopy(this.initValue))
      this.onChange()
    },
    resetChild(level) {
      this.$refs['l' + level].reset_()
    },
    onChange() {
      let val = { fullName: '' }
      for (let i = 1; i < 5; i++) {
        let prop = this.levelList[i - 1]
        if (this[prop].show) {
          val[prop] = this.value_[prop]
        }
        if (val[prop].name) {
          val.fullName += val[prop].name + this.separator
        }
      }
      if (val.fullName) {
        val.fullName = val.fullName.substring(0, val.fullName.length - 1)
      }
      this.$emit('input', val)
      this.$emit('change', val)
    }
  },
  mounted() {
    this.init()
  },
  watch: {
    value: {
      handler() {
        this.init()
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
