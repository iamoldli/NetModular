<template>
  <div>
    <default v-if="type==='default'" :actions="actions"/>
  </div>
</template>
<script>
import Default from './type/default/'
export default {
  name: 'Login',
  components: { Default },
  props: {
    type: {
      type: String,
      default: 'default'
    },
    actions: Object
  }
}
</script>
