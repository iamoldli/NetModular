<template>
  <nm-login :options="options" />
</template>
<script>
import { mapActions, mapState } from 'vuex'
import api from '../../../api/account'
export default {
  computed: {
    ...mapState('module/admin', ['loginSettings']),
    options() {
      return {
        actions: {
          getVerifyCode: api.getVerifyCode,
          login: this.login
        },
        ...this.loginSettings
      }
    }
  },
  methods: {
    ...mapActions('app/token', ['init']),
    async login(params) {
      const data = await api.login(params)
      if (data) {
        this.init(data)
      }
    }
  }
}
</script>
