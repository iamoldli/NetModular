<template>
  <section class="nm-header">
    <section class="nm-header-left">
      <a @click.prevent="sidebarToggle" class="nm-sidebar-toggle-btn">
        <nm-icon :name="sidebarCollapse ? 'indent-left' : 'indent-right'"></nm-icon>
      </a>
      <!--面包屑-->
      <breadcrumb/>
    </section>
    <section class="nm-header-right">
      <!--用户信息-->
      <user-info/>
      <!-- <theme/> -->
      <div class="nm-header-toolbar">
        <!--工具栏-->
        <nm-toolbar/>
      </div>
    </section>
  </section>
</template>
<script>
import { mapState, mapActions } from 'vuex'
import UserInfo from './components/userinfo'
import Breadcrumb from './components/breadcrumb'
export default {
  components: { UserInfo, Breadcrumb },
  computed: {
    ...mapState('app/skins/pretty/sidebar', { sidebarCollapse: 'collapse' })
  },
  methods: {
    ...mapActions('app/skins/pretty/sidebar', { sidebarToggle: 'toggle' })
  }
}
</script>
