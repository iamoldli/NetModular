<template>
  <div class="nm-template-theme">
    <div class="nm-template-sidebar">
      <div class="nm-template-logo-box" :style="'background-color: '+ headerColor"></div>

      <div class="nm-template-menu" :style="'background-color: '+ sideColor"></div>
    </div>
    <div class="nm-template-main">
      <div class="nm-template-header" :style="'background-color: '+ headerColor"></div>
      <div class="nm-template-tabnav"></div>
      <div class="nm-template-content"></div>
    </div>
  </div>
</template>
<script>

export default {
  props: {

    headerColor: {
      type: String,
      default: ''
    },
    sideColor: {
      type: String,
      default: ''
    }

  }
}
</script>

 <style lang="scss">
$theme-border-color: rgba(#909399, 0.4);
$theme-background-color: rgba(#ebf1f6, 0.5);
$theme-height: 8rem;
$theme-width: 10rem;
$sidebar-width: 1.4rem;
$logbox-height: 1.3rem;
$header-height: $logbox-height;
$tabnav-height: 1.2rem;
.nm-template- {
  &theme {
    position: relative;
    height: $theme-height;
    width: $theme-width;
    background-color: $theme-background-color;
  }
  &sidebar {
    position: absolute;
    height: $theme-height;
    width: $sidebar-width;
  }
  &logo-box {
    height: $logbox-height;
    background-color: $theme-background-color;
    border-bottom: 1px solid $theme-border-color;
  }
  &menu {
    height: $theme-height - $logbox-height;
    background-color: $theme-background-color;
  }
  &main {
    position: absolute;
    height: $theme-height;
    width: $theme-width - $sidebar-width;
    left: $sidebar-width;
    background-color: $theme-background-color;
  }

  &header {
    border-bottom: 1px solid $theme-border-color;
    height: $header-height;
    background-color: $theme-background-color;
  }
  &tabnav {
    border-bottom: 1px solid $theme-border-color;
    height: $tabnav-height;
    background-color: $theme-background-color;
  }
  &content {
    height: $theme-height - $header-height - $tabnav-height;
    border-left: 1px solid $theme-border-color;
    background-color: #fff;
  }
}
</style>
