<template>
  <div>
    <default v-if="type==='default'" :actions="actions"/>
    <theme1 v-if="type==='nm-theme'" :actions="actions"/>
  </div>
</template>
<script>
import Default from './type/default/'
import theme1 from './type/theme1/'
export default {
  name: 'Login',
  components: { Default, theme1 },
  props: {
    type: {
      type: String,
      default: 'default'
    },
    actions: Object
  }
}
</script>
