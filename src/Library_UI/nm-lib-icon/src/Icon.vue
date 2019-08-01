<template>
  <svg class="nm-icon" :class="iconClass" aria-hidden="true">
    <use :xlink:href="`#icon-${this.name}`" />
  </svg>
</template>

<script>
export default {
  props: {
    iconClass: String,
    name: String
  }
};
</script>

<style>
.nm-icon {
  width: 1em;
  height: 1em;
  vertical-align: -0.15em;
  fill: currentColor;
  overflow: hidden;
}
</style>
