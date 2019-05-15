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
      <!--工具栏-->
      <div class="nm-header-toolbar">
        <nm-toolbar/>
      </div>
    </section>
  </section>
</template>
<script>
import { mapState, mapActions } from 'vuex'
import Breadcrumb from './components/breadcrumb'
export default {
  components: { Breadcrumb },
  computed: {
    ...mapState('app/skins/pretty/sidebar', { sidebarCollapse: 'collapse' }),
    ...mapState('app/system', ['toolbar'])
  },
  methods: {
    ...mapActions('app/skins/pretty/sidebar', { sidebarToggle: 'toggle' })
  }
}
</script>
