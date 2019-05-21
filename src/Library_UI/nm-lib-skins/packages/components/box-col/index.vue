<template>
  <section :class="['nm-box-col',span ? 'nm-box-col-' + span : '']">
    <slot/>
  </section>
</template>
<script>
export default {
  name: 'BoxCol',
  props: {
    span: Number
  }
}
</script>
