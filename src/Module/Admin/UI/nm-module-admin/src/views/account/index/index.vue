<template>
  <nm-container>
    <nm-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item label="名称：" prop="name">
          <el-input v-model="list.conditions.name" clearable/>
        </el-form-item>
      </template>

      <!--按钮-->
      <template v-slot:toolbar="{total}">
        <nm-button text="添加" icon="add" @click="add(total)" v-nm-has="buttons.add"/>
      </template>

      <!--角色-->
      <template v-slot:col-roles="{row}">
        <template v-if="row.roles&&row.roles.length>0">
          <template v-for="(role,index) in row.roles">
            <nm-button type="text" :key="role.value" :text="role.label"/>
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
        <el-dropdown trigger="click">
          <span class="el-dropdown-link">
            操作
            <i class="el-icon-arrow-down el-icon--right"></i>
          </span>
          <el-dropdown-menu class="nm-list-operation-dropdown" slot="dropdown">
            <el-dropdown-item>
              <nm-button text="编&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;辑" icon="edit" type="text" @click="edit(row)" v-nm-has="buttons.edit"/>
            </el-dropdown-item>
            <el-dropdown-item>
              <nm-button text="重置密码" icon="refresh" type="text" @click="resetPassword(row)" v-nm-has="buttons.resetPassword"/>
            </el-dropdown-item>
            <el-dropdown-item>
              <nm-button-delete text="删&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;除" :action="removeAction" :id="row.id" @success="refresh" v-nm-has="buttons.del"/>
            </el-dropdown-item>
          </el-dropdown-menu>
        </el-dropdown>
      </template>
    </nm-list>

    <!--添加-->
    <add-page :visible.sync="addPage.visible" :sort="addPage.sort" @success="refresh"/>
    <!--编辑-->
    <edit-page :visible.sync="editPage.visible" :id="editPage.id" @success="refresh"/>
  </nm-container>
</template>
<script>
import api from '../../../api/account.js'
import page from './page'
import cols from './cols'
import AddPage from '../add'
import EditPage from '../edit'

export default {
  name: page.name,
  components: { AddPage, EditPage },
  data () {
    return {
      list: {
        title: page.title,
        cols,
        action: api.query,
        conditions: {
          name: ''
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
  methods: {
    refresh () {
      this.$refs.list.refresh()
    },
    add (total) {
      this.addPage.sort = total
      this.addPage.visible = true
    },
    edit (row) {
      this.editPage = {
        id: row.id,
        visible: true
      }
    },
    resetPassword (row) {
      this._confirm(`您确定要重置账户(${row.name})的密码吗？`).then(() => {
        api.resetPassword(row.id).then(data => {
          this._success('已重置')
        })
      })
    }
  }
}
</script>
