<template>
  <section class="nm-details-cell" :style="{width}">
    <section class="nm-details-label">
      <slot name="label">
        <span>{{label}}</span>
      </slot>
    </section>
    <section class="nm-details-content">
      <slot/>
    </section>
  </section>
</template>
<script>
export default {
  name: 'DetailsCell',
  props: {
    label: String,
    width: String
  }
}
</script>
