<template>
  <section class="nm-sidebar" :class="{collapse:collapse}">
    <section class="nm-logo-box">
      <img :src="sys.logo" class="nm-logo-img" :alt="sys.title" :title="sys.title" />
      <label class="nm-logo-text">{{sys.title}}</label>
    </section>
    <section class="nm-menu-wapper">
      <nm-menus />
    </section>
  </section>
</template>
<script>
import NmMenus from '../menus'
import { mapState } from 'vuex'
export default {
  components: { NmMenus },
  computed: {
    ...mapState('app/system', { sys: state => state }),
    ...mapState('app/skins/pretty/sidebar', ['collapse'])
  }
}
</script>
