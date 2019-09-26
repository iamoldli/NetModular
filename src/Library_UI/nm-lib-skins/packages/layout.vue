<template>
  <div id="app" :class="current.theme">
    <template v-if="inFrame">
      <component v-if="initialized" :is="`skin-${current.name.toLowerCase()}`"></component>
    </template>
    <template v-else>
      <router-view />
    </template>

    <!--对话框菜单-->
    <nm-dialog v-bind="dialogMenu" :visible.sync="dialogNemuVisible">
      <iframe :src="dialogMenu.url" style="width:100%;height:100%"></iframe>
    </nm-dialog>
  </div>
</template>
<script>
import { mapState, mapActions } from 'vuex'
export default {
  computed: {
    ...mapState('app/skins', ['initialized', 'current']),
    ...mapState('app/dialog-menu', { dialogMenu: state => state }),
    // 是否在框架中显示
    inFrame() {
      return !this.$route.meta || this.$route.meta.frameIn !== false
    },
    dialogNemuVisible: {
      get() {
        return this.dialogMenu.visible
      },
      set() {
        this.dialogMenuClose()
      }
    }
  },
  methods: {
    ...mapActions('app/dialog-menu', { dialogMenuClose: 'close' })
  }
}
</script>
