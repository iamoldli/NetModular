<template>
  <td-container>
    <td-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item label="名称：" prop="name">
          <el-input v-model="list.model.name" clearable/>
        </el-form-item>
        <el-form-item label="编码：" prop="code">
          <el-input v-model="list.model.code" clearable/>
        </el-form-item>
      </template>

      <!--按钮-->
      <template v-slot:querybar-buttons>
        <td-button type="success" text="添加" icon="add" @click="add" v-td-has="buttons.add"/>
      </template>

      <!--配置类-->
      <template v-slot:col-name="{row}">
        <td-button :text="row.name" type="text" @click="manageClass(row)"/>
      </template>

      <!--是否显示-->
      <template v-slot:col-isShow="{row}">{{row.isShow?'是':'否'}}</template>

      <!--操作列-->
      <template v-slot:col-operation="{row}">
        <td-button text="编辑" icon="edit" type="text" @click="edit(row)" v-td-has="buttons.edit"/>
        <td-button-delete :action="removeAction" :id="row.id" @success="refresh" v-td-has="buttons.del"/>
        <td-button text="生成" icon="run" type="text" @click="buildCode(row)" v-td-has="buttons.buildCode"/>
      </template>
    </td-list>

    <!--添加-->
    <add-page :visible.sync="dialog.add" @success="refresh"/>
    <!--编辑-->
    <edit-page :id="curr.id" :visible.sync="dialog.edit" @success="refresh"/>
    <!--类管理-->
    <class-page :project="curr" :visible.sync="dialog.class"/>
  </td-container>
</template>
<script>
import { mixins } from 'td-lib-skins'
import api from '../../../api/project.js'
import page from './page'
import cols from './cols'
import AddPage from '../components/add'
import EditPage from '../components/edit'
import ClassPage from '../../class/index'

export default {
  name: page.name,
  mixins: [mixins.loading],
  components: { AddPage, EditPage, ClassPage },
  data() {
    return {
      curr: { id: '' },
      list: {
        title: page.title,
        cols,
        action: api.query,
        operationWidth: 200,
        model: {
          name: '',
          code: ''
        }
      },
      removeAction: api.remove,
      dialog: {
        add: false,
        edit: false,
        class: false
      },
      buttons: page.buttons
    }
  },
  methods: {
    refresh() {
      this.$refs.list.refresh()
    },
    add() {
      this.dialog.add = true
    },
    edit(row) {
      this.curr = row
      this.dialog.edit = true
    },
    buildCode(row) {
      let loading = this._loading('正在努力生成代码，请稍后...')
      api.buildCode({ id: row.id }).then(data => {
        const url = window.URL.createObjectURL(data)
        const link = document.createElement('a')
        link.href = url
        link.setAttribute('download', row.code + '.zip')
        document.body.appendChild(link)
        link.click()
        document.body.removeChild(link)
        loading.close()
      }).catch(() => {
        loading.close()
      })
    },
    manageClass(row) {
      this.curr = row
      this.dialog.class = true
    }
  }
}
</script>
