<template>
  <div class="nm-header-userinfo">
    <el-dropdown trigger="click">
      <span class="el-dropdown-link">
        {{userName}}
        <i class="el-icon-arrow-down el-icon--right"></i>
      </span>

      <el-dropdown-menu class="nm-header-userinfo-dropdown" slot="dropdown">
        <el-dropdown-item>
          <nm-button type="text" text="个人信息" icon="user"/>
        </el-dropdown-item>
        <el-dropdown-item>
          <nm-button type="text" text="修改密码" icon="password" @click="updatePassword.visiable = true"/>
        </el-dropdown-item>
        <el-dropdown-item divided>
          <nm-button type="text" text="退&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;出" icon="logout" @click="onLogout"/>
        </el-dropdown-item>
      </el-dropdown-menu>
    </el-dropdown>

    <!--修改密码-->
    <nm-update-password :visible.sync="updatePassword.visiable"/>
  </div>
</template>
<script>
import { mapState, mapActions } from 'vuex'
import NmUpdatePassword from '../../../../../../components/update-password'
export default {
  components: { NmUpdatePassword },
  data () {
    return {
      updatePassword: {
        visiable: false
      }
    }
  },
  computed: {
    ...mapState('app/account', { userName: 'name' })
  },
  methods: {
    ...mapActions('app/system', ['logout']),
    onLogout () {
      this._confirm('您确认要退出登录吗').then(() => {
        this.logout()
      })
    }
  }
}
</script>
