<template>
  <nm-container>
    <nm-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item label="名称：" prop="name">
          <el-input v-model="list.model.name" clearable />
        </el-form-item>
        <el-form-item label="编码：" prop="code">
          <el-input v-model="list.model.code" clearable />
        </el-form-item>
      </template>

      <!--按钮-->
      <template v-slot:querybar-buttons>
        <nm-button-has :options="buttons.add" @click="add" />
      </template>

      <!--配置类-->
      <template v-slot:col-name="{row}">
        <nm-button :text="row.name" type="text" @click="manageClass(row)" />
      </template>

      <!--是否显示-->
      <template v-slot:col-isShow="{row}">{{row.isShow?'是':'否'}}</template>

      <!--操作列-->
      <template v-slot:col-operation="{row}">
        <nm-button-has :options="buttons.edi" @click="edit(row)" />
        <nm-button-delete :options="buttons.del" :action="removeAction" :id="row.id" @success="refresh" />
        <nm-button-has :options="buttons.buildCode" @click="buildCode(row)" />
      </template>
    </nm-list>

    <!--添加-->
    <add-page :visible.sync="dialog.add" @success="refresh" />
    <!--编辑-->
    <edit-page :id="curr.id" :visible.sync="dialog.edit" @success="refresh" />
    <!--类管理-->
    <class-page :project="curr" :visible.sync="dialog.class" />
  </nm-container>
</template>
<script>
import { mixins } from 'nm-lib-skins'
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
      api.buildCode({ id: row.id }).then(() => {
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
