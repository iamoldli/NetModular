<template>
  <nm-login :actions="actions" :type="loginSettings.type" :account-type="loginSettings.accountType" />
</template>
<script>
import { mapActions, mapState } from 'vuex'
import api from '../../../api/account'
export default {
  computed: {
    ...mapState('module/admin', ['loginSettings']),
    actions() {
      return {
        getVerifyCode: api.getVerifyCode,
        login: this.login
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
