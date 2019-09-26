<template>
  <section class="nm-menus">
    <div class="no-menus" v-if="!menus||menus.length<1">
      <nm-icon name="form" />
      <p>没有菜单</p>
    </div>
    <el-scrollbar v-else>
      <el-menu :default-active="active" :unique-opened="menuUniqueOpened" :collapse="collapse" :collapse-transition="false">
        <template v-for="item in menus">
          <menu-item v-if="item.show" :key="item.id" :menu="item" />
        </template>
      </el-menu>
    </el-scrollbar>
  </section>
</template>
<script>
import MenuItem from '../../../pretty/components/menus/item'
import { mapState } from 'vuex'
export default {
  components: { MenuItem },
  computed: {
    ...mapState('app/system', ['menuUniqueOpened']),
    ...mapState('app/skins/classics/sidebar', ['collapse']),
    ...mapState('app/skins/classics', ['menus']),
    ...mapState('app/account', ['routeMenus']),
    ...mapState('app/page', ['current']),
    active: {
      get() {
        if (this.current.name && this.routeMenus) {
          let routeMenu = this.routeMenus.get(this.current.name)

          if (routeMenu) {
            return routeMenu.menu.id
          }
        }
        return '-1'
      },
      set() { }
    }
  }
}
</script>
