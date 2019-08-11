<template>
  <div class="nm-toolbar-item">
    <nm-button class="nm-toolbar-button" type="text" @click="onLogout">
      <nm-icon name="logout"/>
    </nm-button>
  </div>
</template>

<script>
import { mapActions } from 'vuex'
export default {
  methods: {
    ...mapActions('app/system', ['logout']),
    onLogout () {
      this._confirm('您确认要退出登录吗').then(() => {
        this.logout(this.$route.fullPath)
      })
    }
  }
}
</script>
