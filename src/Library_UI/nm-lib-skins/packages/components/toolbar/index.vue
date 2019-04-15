<template>
  <div class="nm-toolbar">
    <nm-skin-toggle v-if="toolbar.skin"/>
    <nm-fullscreen v-if="toolbar.fullscreen"/>
  </div>
</template>
<script>
import { mapState } from 'vuex'
import NmFullscreen from './components/fullscreen'
import NmSkinToggle from './components/skin-toggle'
export default {
  name: 'Toolbar',
  components: { NmFullscreen, NmSkinToggle },
  computed: {
    ...mapState('app/system', ['toolbar'])
  }
}
</script>
