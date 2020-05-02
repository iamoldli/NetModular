<template>
  <nm-container>
    <nm-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item label="用户名：" prop="userName">
          <el-input v-model="list.model.userName" clearable />
        </el-form-item>
        <el-form-item label="名称：" prop="name">
          <el-input v-model="list.model.name" clearable />
        </el-form-item>
      </template>

      <!--高级查询-->
      <template v-slot:querybar-advanced>
        <el-row>
          <el-col :span="20" :offset="1">
            <el-form-item label="手机号：" prop="phone">
              <el-input v-model="list.model.phone" clearable />
            </el-form-item>
            <el-form-item label="邮箱：" prop="email">
              <el-input v-model="list.model.name" clearable />
            </el-form-item>
          </el-col>
        </el-row>
      </template>

      <!--按钮-->
      <template v-slot:querybar-buttons="{ total }">
        <nm-button v-bind="buttons.add" @click="add(total)" />
      </template>

      <!--角色-->
      <template v-slot:col-roles="{ row }">
        <template v-if="row.roles && row.roles.length > 0">
          <template v-for="(role, index) in row.roles">
            <el-tag :key="role.value" :text="role.label">{{ role.label }}</el-tag>
            <template v-if="index < row.roles.length - 1">、</template>
          </template>
        </template>
        <span v-else>未绑定</span>
      </template>

      <!--状态-->
      <template v-slot:col-status="{ row }">
        <el-tag v-if="row.status === 0" type="info">未激活</el-tag>
        <el-tag v-else-if="row.status === 1" type="success">正常</el-tag>
        <el-tag v-else-if="row.status === 2" type="warning">禁用</el-tag>
        <el-tag v-else-if="row.status === 3" type="danger">注销</el-tag>
      </template>

      <!--登录时间-->
      <template v-slot:col-loginTime="{ row }">{{ row.status === 0 ? '未登录' : row.loginTime }}</template>
      <template v-slot:col-loginIP="{ row }">{{ row.status === 0 ? '未登录' : row.loginIP }}</template>

      <!--操作列-->
      <template v-slot:col-operation="{ row }">
        <nm-button v-if="!row.isLock && row.status === 0" v-bind="buttons.active" @click="active(row)" />
        <nm-button v-if="!row.isLock" v-bind="buttons.edit" @click="edit(row)" />
        <nm-button v-if="!row.isLock" v-bind="buttons.resetPassword" @click="resetPassword(row)" />
        <nm-button-delete v-if="!row.isLock" v-bind="buttons.del" :disabled="row.id === accountId" :action="removeAction" :id="row.id" @success="refresh" />
        <nm-button v-if="row.isLock" type="text" text="锁定" :disabled="true" />
      </template>
    </nm-list>

    <!--保存页-->
    <save-page :id="curr.id" :visible.sync="dialog.save" @success="refresh" />
  </nm-container>
</template>
<script>
import { mapState } from 'vuex'
import { mixins } from 'netmodular-ui'
import page from './page'
import cols from './cols'
import SavePage from '../components/save'

// 接口
const api = $api.admin.account

export default {
  name: page.name,
  mixins: [mixins.list],
  components: { SavePage },
  data() {
    return {
      list: {
        title: page.title,
        cols,
        action: api.query,
        operationWidth: '270',
        advanced: {
          enabled: true,
          width: '400px'
        },
        model: {
          userName: '',
          name: '',
          phone: '',
          email: ''
        }
      },
      removeAction: api.remove,
      buttons: page.buttons
    }
  },
  computed: {
    ...mapState('app/account', { accountId: 'id' })
  },
  methods: {
    resetPassword(row) {
      this._confirm(`您确定要重置账户(${row.name})的密码吗？`).then(() => {
        api.resetPassword(row.id).then(() => {
          this._success('已重置')
        })
      })
    },
    active(row) {
      this._confirm(`您确定要激活账户(${row.name})吗？`).then(() => {
        api.active(row.id).then(() => {
          this._success('已激活')
          row.status = 1
        })
      })
    }
  }
}
</script>
