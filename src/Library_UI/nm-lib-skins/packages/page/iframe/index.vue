<template>
  <nm-container>
    <iframe :src="url" style="width:100%;height:100%"/>
  </nm-container>
</template>
<script>
export default {
  name: '_iframe',
  computed: {
    url () {
      return this.$route.query.url
    }
  }
}
</script>
