<template>
  <div>
    <default v-if="options.type==='default'" :options="options" />
  </div>
</template>
<script>
import Default from './type/default/'
export default {
  name: 'Login',
  components: { Default },
  props: {
    options: {
      type: Object,
      required: true
    }
  }
}
</script>
