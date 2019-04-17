<template>
  <section class="nm-menus">
    <el-scrollbar>
      <el-menu :default-active="active" :collapse="collapse" :collapse-transition="false">
        <template v-for="item in menus">
          <menu-item v-if="item.show" :key="item.id" :menu="item"/>
        </template>
      </el-menu>
    </el-scrollbar>
  </section>
</template>
<script>
import MenuItem from './item'
import { mapState } from 'vuex'
export default {
  data () {
    return {
      active: ''
    }
  },
  components: { MenuItem },
  computed: {
    ...mapState('app/account', ['menus']),
    ...mapState('app/skins/pretty/sidebar', ['collapse'])
  },
  created () {
    this.setActive()
  },
  methods: {
    /**
     * @description 递归获取菜单的激活index
     * @param {String} routeName 路由名称
     * @param {Arrary} menus 菜单列表
     */
    getActive (routeName, menus) {
      let id = ''
      if (menus && menus.length > 0) {
        for (let i = 0; i < menus.length; i++) {
          const menu = menus[i]
          if (menu.type === 0) {
            id = this.getActive(routeName, menu.children)
          } else {
            if (menu.routeName === routeName) {
              id += menu.id
              break
            }
          }
          if (id !== '') break
        }
      }
      return id
    },
    /**
     * @description 获取激活菜单的index
     */
    setActive () {
      const routeName = this.$route.name
      if (routeName) {
        this.active = this.getActive(routeName, this.menus)
      }
    }
  },
  watch: {
    /**
     * @description 监听路由，动态更改菜单栏的激活index
     */
    $route () {
      this.setActive(this.menus)
    }
  }
}
</script>
