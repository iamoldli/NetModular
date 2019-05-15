<template>
  <nm-container>
    <h1>欢迎登陆{{title}}~</h1>
  </nm-container>
</template>
<script>
import { mapState } from 'vuex'
export default {
  computed: {
    ...mapState('app/system', ['title'])
  }
}
</script>
