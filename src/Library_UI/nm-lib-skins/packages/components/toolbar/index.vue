<template>
  <div class="nm-toolbar">
    <nm-userinfo v-if="toolbar.userInfo"/>
    <nm-skin-toggle v-if="toolbar.skin"/>
    <nm-fullscreen v-if="toolbar.fullscreen"/>
    <nm-logout v-if="toolbar.logout"/>
  </div>
</template>
<script>
import { mapState } from 'vuex'
import NmFullscreen from './components/fullscreen'
import NmLogout from './components/logout'
import NmSkinToggle from './components/skin-toggle'
import NmUserinfo from './components/userInfo'
export default {
  name: 'Toolbar',
  components: { NmFullscreen, NmLogout, NmSkinToggle, NmUserinfo },
  computed: {
    ...mapState('app/system', ['toolbar'])
  }
}
</script>
