<template>
  <section :class="['nm-main',fontSize,sidebarCollapse?'collapse':'']">
    <section class="nm-main-left">
      <nm-menus />
    </section>
    <section class="nm-main-right">
      <div class="nm-main-right-top">
        <div>
          <nm-tabnav>
            <template v-slot:before>
              <div class="nm-sidebar-toggle-btn">
                <a @click.prevent="sidebarToggle">
                  <nm-icon :name="sidebarCollapse ? 'indent-left' : 'indent-right'"></nm-icon>
                </a>
              </div>
            </template>
          </nm-tabnav>
        </div>
      </div>
      <section class="nm-content">
        <transition name="fade-transverse">
          <keep-alive :include="keepAlive">
            <router-view :key="$route.path" />
          </keep-alive>
        </transition>
      </section>
    </section>
  </section>
</template>
<script>
import { mapState, mapActions } from 'vuex'
import NmMenus from '../menus'
export default {
  components: { NmMenus },
  computed: {
    ...mapState('app/skins/classics/sidebar', { sidebarCollapse: 'collapse' }),
    ...mapState('app/page', ['keepAlive'])
  },
  methods: {
    ...mapActions('app/skins/classics/sidebar', { sidebarToggle: 'toggle' })
  }
}
</script>
