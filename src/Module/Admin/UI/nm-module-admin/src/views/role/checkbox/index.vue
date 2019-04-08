<template>
  <el-checkbox-group v-model="checked_">
    <el-checkbox v-for="item in options" :key="item.value" border :label="item.value" @change="onChange">{{item.name}}</el-checkbox>
  </el-checkbox-group>
</template>
<script>
import api from '../../../api/role.js'
export default {
  data () {
    return {
      options: [],
      checked_: this.checked
    }
  },
  props: {
    checked: Array
  },
  methods: {
    refresh () {
      api.select().then(data => {
        this.options = data
      })
    },
    onChange (value) {
      console.log(value)
    }
  },
  watch: {
    checked () {
      this.checked_ = this.checked
    }
  }
}
</script>
