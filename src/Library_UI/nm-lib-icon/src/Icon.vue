<template>
  <svg class="nm-icon" :class="iconClass" aria-hidden="true">
    <use :xlink:href="Name"></use>
  </svg>
</template>

<script>
export default {
  props: {
    iconClass: {
      type: String
    },
    name: {
      type: String,
      required: true
    }
  },
  computed: {
    Name () {
      return `#icon-${this.name}`;
    }
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
