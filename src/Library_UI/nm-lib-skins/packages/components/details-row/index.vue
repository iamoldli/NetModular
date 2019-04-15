<template>
  <section ref="row" class="nm-details-row" :class="'cols-'+cols">
    <slot/>
  </section>
</template>
<script>
export default {
  name: 'DetailsRow',
  props: {
    cols: {
      type: Number,
      default: 1
    }
  }
}
</script>
