<template>
  <div class="nm-color-picker">
    <div class="nm-color-picker-input">
      <el-input placeholder="请选择颜色" v-model="color" :size="fontSize" :disabled="disabled" :clearable="clearable"></el-input>
    </div>
    <div class="nm-color-picker-color">
      <el-color-picker
        v-model="color"
        :size="fontSize"
        :disabled="disabled"
        :show-alpha="showAlpha"
        :color-format="colorFormat"
        :popper-class="popperClass"
        :predefine="predefine"
        @change="onChange"
      ></el-color-picker>
    </div>
  </div>
</template>
<script>
export default {
  name: 'ColorPicker',
  data () {
    return {
      color: this.value
    }
  },
  props: {
    value: {
      type: String
    },
    /** 可清空的 */
    clearable: {
      type: Boolean,
      default: true
    },
    /** 是否禁用 */
    disabled: Boolean,
    /** 尺寸 */
    size: String,
    /** 是否支持透明度选择 */
    showAlpha: Boolean,
    /** 写入 v-model 的颜色的格式 */
    colorFormat: String,
    /** ColorPicker 下拉框的类名 */
    popperClass: String,
    /** 预定义颜色 */
    predefine: Array
  },
  methods: {
    onChange (val) {
      this.$emit('input', val)
      this.$emit('change', val)
    }
  },
  watch: {
    value () {
      this.color = this.value
    }
  }
}
</script>
