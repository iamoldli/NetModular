<template>
  <nm-login :actions="actions"/>
</template>
<script>
import api from '../../../api/account'
export default {
  data () {
    return {
      actions: {
        getVerifyCode: api.getVerifyCode,
        login: api.login
      }
    }
  }
}
</script>
