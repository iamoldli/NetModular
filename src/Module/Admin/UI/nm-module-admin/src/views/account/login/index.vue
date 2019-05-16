<template>
  <nm-login :actions="actions"/>
</template>
<script>
import { mapActions } from 'vuex'
import api from '../../../api/account'
export default {
  computed: {
    actions () {
      return {
        getVerifyCode: api.getVerifyCode,
        login: this.login
      }
    }
  },
  methods: {
    ...mapActions('module/admin/token', ['init']),
    async login (params) {
      const data = await api.login(params)
      if (data) {
        this.init(data)
      }
    }
  }
}
</script>
