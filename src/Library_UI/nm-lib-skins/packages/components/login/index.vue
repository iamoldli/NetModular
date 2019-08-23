<template>
  <div>
    <default v-if="type==='default'" :account-type="accountType" :actions="actions" />
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
    accountType: {
      type: Number,
      default: 0
    },
    actions: Object
  }
}
</script>
