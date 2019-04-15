<template>
  <el-breadcrumb class="nm-breadcrumb" separator="/">
    <transition-group name="breadcrumb">
      <el-breadcrumb-item v-for="(item,index) in breadcrumb" :key="`${index}_${item.path}`" :to="item.path">{{item.title}}</el-breadcrumb-item>
    </transition-group>
  </el-breadcrumb>
</template>
<script>
export default {
  computed: {
    breadcrumb () {
      if (this.$route.meta.breadcrumb) {
        let breadcrumb = [...this.$route.meta.breadcrumb]
        breadcrumb.push({
          title: this.$route.meta.title,
          path: ''
        })
        return breadcrumb
      } else {
        return [{
          title: '首页',
          path: '/'
        }]
      }
    }
  }
}
</script>
