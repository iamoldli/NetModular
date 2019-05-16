<template>
  <el-dropdown :hide-on-click="false">
    <span class="el-dropdown-link">
      <nm-icon name="skin"/>主题
      <i class="el-icon-arrow-down el-icon--right"></i>
    </span>
    <el-dropdown-menu v-slot:dropdown>
      <el-dropdown-item v-for="info in list" v-bind:key="info.name">
        <span v-html="info.name" @click="themeChange(info)"></span>
      </el-dropdown-item>
    </el-dropdown-menu>
  </el-dropdown>
</template>
<script>
import list from './list'
import { mapState, mapMutations } from 'vuex'
export default {
  data () {
    return {
      list
    }
  },
  computed: {
    ...mapState('app/skins', ['current'])
  },
  methods:
    {
      ...mapMutations('app/skins', ['themeChange'])
    }
}
</script>
<style>
.el-dropdown-link {
  cursor: pointer;
  color: #409eff;
}
.el-icon-arrow-down {
  font-size: 12px;
}
</style>
