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
      <template v-slot:querybar-buttons="{total}">
        <nm-button-has :options="buttons.add" @click="add(total)" />
      </template>

      <!--角色-->
      <template v-slot:col-roles="{row}">
        <template v-if="row.roles&&row.roles.length>0">
          <template v-for="(role,index) in row.roles">
            <el-tag :key="role.value" :text="role.label">{{role.label}}</el-tag>
            <template v-if="index < row.roles.length - 1">、</template>
          </template>
        </template>
        <span v-else>未绑定</span>
      </template>

      <!--状态-->
      <template v-slot:col-status="{row}">
        <el-tag v-if="row.status===0" type="info">未激活</el-tag>
        <el-tag v-else-if="row.status===1" type="success">正常</el-tag>
        <el-tag v-else-if="row.status===2" type="warning">禁用</el-tag>
        <el-tag v-else-if="row.status===3" type="danger">注销</el-tag>
      </template>

      <!--登录时间-->
      <template v-slot:col-loginTime="{row}">{{row.status===0?'未登录':row.loginTime}}</template>
      <template v-slot:col-loginIP="{row}">{{row.status===0?'未登录':row.loginIP}}</template>

      <!--操作列-->
      <template v-slot:col-operation="{row}">
        <el-dropdown trigger="click" v-if="!row.isLock">
          <span class="el-dropdown-link">
            操作
            <i class="el-icon-arrow-down el-icon--right"></i>
          </span>
          <el-dropdown-menu class="nm-list-operation-dropdown" slot="dropdown">
            <el-dropdown-item>
              <nm-button-has :options="buttons.edit" @click="edit(row)" />
            </el-dropdown-item>
            <el-dropdown-item>
              <nm-button-has :options="buttons.resetPassword" @click="resetPassword(row)" />
            </el-dropdown-item>
            <el-dropdown-item>
              <nm-button-delete :options="buttons.del" :disabled="row.id===accountId" :action="removeAction" :id="row.id" @success="refresh" />
            </el-dropdown-item>
          </el-dropdown-menu>
        </el-dropdown>
        <nm-button v-else type="text" text="锁定" :disabled="true" />
      </template>
    </nm-list>

    <!--添加-->
    <add-page :visible.sync="addPage.visible" :sort="addPage.sort" @success="refresh" />
    <!--编辑-->
    <edit-page :visible.sync="editPage.visible" :id="editPage.id" @success="refresh" />
  </nm-container>
</template>
<script>
import { mapState } from 'vuex'
import page from './page'
import cols from './cols'
import AddPage from '../components/add'
import EditPage from '../components/edit'

// 接口
const api = $api.admin.account

export default {
  name: page.name,
  components: { AddPage, EditPage },
  data() {
    return {
      list: {
        title: page.title,
        cols,
        action: api.query,
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
      addPage: {
        visible: false,
        sort: 0
      },
      editPage: {
        visible: false,
        id: ''
      },
      removeAction: api.remove,
      buttons: page.buttons
    }
  },
  computed: {
    ...mapState('app/account', { accountId: 'id' })
  },
  methods: {
    refresh() {
      this.$refs.list.refresh()
    },
    add(total) {
      this.addPage.sort = total
      this.addPage.visible = true
    },
    edit(row) {
      this.editPage = {
        id: row.id,
        visible: true
      }
    },
    resetPassword(row) {
      this._confirm(`您确定要重置账户(${row.name})的密码吗？`).then(() => {
        api.resetPassword(row.id).then(data => {
          this._success('已重置')
        })
      })
    }
  }
}
</script>
