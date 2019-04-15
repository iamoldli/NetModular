<template>
  <div class="nm-wrapper">
    <nm-sidebar/>
    <nm-main/>
  </div>
</template>
<script>
import NmSidebar from './components/sidebar'
import NmMain from './components/main'
import { mapState } from 'vuex'
export default {
  components: { NmSidebar, NmMain },
  computed: {
    ...mapState('app/skins', ['current']),
    themeClass () {
      return this.current.theme
    }
  },
  mounted () {
    this.$nextTick(() => {
      this.setbodyClass()
    })
  },
  methods:
  {
    setbodyClass () { this.$nextTick(() => { document.body.className = 'nm-skin-pretty ' + this.current.theme }) }
  },
  watch: {
    themeClass (val) {
      this.setbodyClass()
    }
  }
}
</script>
