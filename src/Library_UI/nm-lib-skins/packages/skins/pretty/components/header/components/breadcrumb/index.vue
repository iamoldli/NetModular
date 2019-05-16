<template>
  <el-breadcrumb class="nm-breadcrumb" separator="/">
    <transition-group name="breadcrumb">
      <el-breadcrumb-item v-for="(item,index) in breadcrumb" :key="`${index}_${item.route}`" :to="item.route">{{item.title}}</el-breadcrumb-item>
    </transition-group>
  </el-breadcrumb>
</template>
<script>
import { mapState } from 'vuex'
export default {
  computed: {
    ...mapState('app/account', ['routeMenus']),
    ...mapState('app/page', ['default']),
    breadcrumb () {
      let route = this.$route
      let bc = [{
        title: '首页',
        route: '/'
      }]

      if (!route.name || this.default === route.path) { return bc }

      if (this.routeMenus) {
        const obj = this.routeMenus.get(route.name)
        if (obj) {
          bc = bc.concat(obj.breadcrumb)
        }
      }

      // 通过路由自定义的最后一个面包屑
      if (route.params && route.params.bc_) {
        bc.push({
          title: route.params.bc_,
          route: ''
        })
      }
      return bc
    }
  }
}
</script>
