<template>
  <!--节点菜单-->
  <el-submenu v-if="menu.type===0" :index="menu.id+''">
    <template v-slot:title>
      <nm-icon :name="menu.icon" class="nm-menus-icon" :style="{color:menu.iconColor}"/>
      <span>{{ menu.name }}</span>
    </template>
    <template v-for="item in menu.children">
      <menu-item v-if="item.show" :key="item.id" :menu="item" :parent-index="menu.id"/>
    </template>
  </el-submenu>

  <!--链接菜单-->
  <el-menu-item v-else :index="menu.id+''" :parent-index="parentIndex" @click="go(menu)">
    <nm-icon :name="menu.icon" class="nm-menus-icon" :style="{color:menu.iconColor}"/>
    <span>{{ menu.name }}</span>
  </el-menu-item>
</template>
<script>
import { mapActions } from 'vuex'
export default {
  name: 'MenuItem',
  data() { return {} },
  props: {
    parentIndex: {
      type: String,
      default: ''
    },
    menu: {}
  },
  methods: {
    ...mapActions('app/dialog-menu', { dialogMenuOpen: 'open' }),
    go(menu) {
      // 1、站内路由 2、站外链接
      if (menu.type === 1) {
        this.openRoute(menu)
      } else if (menu.type === 2) {
        this.openLink(menu)
      }
    },
    // 打开路由菜单
    openRoute(menu) {
      this.$router.push({ name: menu.routeName, params: { tn_: menu.name } })
    },
    // 打开链接菜单
    openLink(menu) {
      const target = menu.target
      if (!target || target === 0) {
        window.open(menu.url, '_blank')
      } else if (target === 1) {
        window.open(menu.url, '_self')
      } else if (target === 2) {
        // Dialog模式
        this.dialogMenuOpen({
          title: menu.name,
          icon: menu.icon,
          url: menu.url,
          width: menu.dialogWidth,
          height: menu.dialogHeight,
          fullscreen: menu.dialogFullscreen
        })
      } else if (target === 3 || target === 4) {
        // 当前皮肤下，容器内和内容区一样
        this.$router.push({ name: 'Iframe', params: { url: encodeURI(menu.url), tn_: menu.name } })
      }
    },
    getIndex(index) {
      if (this.parentIndex === 0) { return index + '' }
      return this.parentIndex + '-' + index
    }
  }
}
</script>
