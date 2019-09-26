<template>
  <div class="nm-wrapper">
    <nm-sidebar />
    <nm-main />
  </div>
</template>
<script>
import NmSidebar from './components/sidebar'
import NmMain from './components/main'
export default {
  components: { NmSidebar, NmMain }
}
</script>
<style lang="scss">
@import "./styles/_index.scss";
</style>
