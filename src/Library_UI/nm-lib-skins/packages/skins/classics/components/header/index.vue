<template>
  <section class="nm-header">
    <!--logo-->
    <section class="nm-header-logo">
      <img :src="sys.logo" class="nm-header-logo-img" :alt="sys.title" :title="sys.title" />
      <div class="nm-header-logo-text">{{sys.title}}</div>
    </section>
    <!--导航栏-->
    <section class="nm-header-nav">
      <ul>
        <template v-for="menu in menus">
          <li :class="['nm-header-nav-item',menu.id===curr?'active':'']" :key="menu.id">
            <a href="javascript:void(0)" @click.prevent="onNavClick(menu)">
              <nm-icon class="nm-header-nav-item-icon" :name="menu.icon" :style="{color:menu.iconColor}" />
              <span class="nm-header-nav-item-text">{{ menu.name }}</span>
            </a>
          </li>
        </template>
      </ul>
    </section>
    <!--工具栏-->
    <section class="nm-header-toolbar">
      <nm-toolbar />
    </section>
  </section>
</template>
<script>
import { mapState, mapMutations } from 'vuex'
import { open } from '../../../../utils/menu'

export default {
  data() {
    return {
      curr: ''
    }
  },
  computed: {
    ...mapState('app/system', { sys: state => state }),
    ...mapState('app/account', ['menus', 'routeMenus']),
    ...mapState('app/page', ['current'])
  },
  methods: {
    ...mapMutations('app/skins/classics', ['setMenus']),
    onNavClick(menu) {
      this.curr = menu.id
      // 节点类型
      if (menu.type === 0) {
        if (menu.children) {
          this.setMenus(menu.children)
          if (menu.children && menu.children[0]) {
            open(this.$router, menu.children[0])
          }
        } else {
          this.setMenus([])
        }
      } else {
        open(this.$router, menu)
      }
    }
  },
  watch: {
    'current'() {
      if (this.current.name && this.routeMenus) {
        let routeMenu = this.routeMenus.get(this.current.name)
        if (routeMenu) {
          for (var i = 0; i < this.menus.length; i++) {
            if (this.menus[i].id === routeMenu.menu.rootId) {
              this.curr = this.menus[i].id
              this.setMenus(this.menus[i].children)
              break
            }
          }
        }
      }
    }
  }
}
</script>
