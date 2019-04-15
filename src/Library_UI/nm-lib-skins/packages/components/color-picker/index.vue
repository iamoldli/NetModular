<template>
  <div class="nm-color-picker">
    <div class="nm-color-picker-input">
      <el-input placeholder="请选择颜色" v-model="color" clearable></el-input>
    </div>
    <div class="nm-color-picker-color">
      <el-color-picker v-model="color" @change="onChange"></el-color-picker>
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
  props: ['value'],
  methods: {
    onChange (val) {
      this.$emit('input', val)
    }
  },
  watch: {
    value () {
      this.color = this.value
    }
  }
}
</script>
