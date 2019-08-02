<template>
  <nm-container>
    <nm-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
      </template>

      <!--按钮-->
      <template v-slot:querybar-buttons>
        <nm-button-has :options="buttons.add" @click="add(total)" />
      </template>

      <!--自定义列-->
      <!-- <template v-slot:col-name="{row}">
        <nm-button :text="row.name" type="text" />
      </template> -->

      <!--操作列-->
      <template v-slot:col-operation="{row}">
        <nm-button-has :options="buttons.edit" @click="edit(row)" />
        <nm-button-delete :options="buttons.del" :id="row.id" :action="removeAction" @success="refresh"/>
      </template>
    </nm-list>

    <!--添加-->
    <add-page :visible.sync="dialog.add" @success="refresh"/>
    <!--编辑-->
    <edit-page :id="curr.id" :visible.sync="dialog.edit" @success="refresh"/>
  </nm-container>
</template>
<script>
import api from '../../../api/Group'
import page from './page'
import cols from './cols'
import AddPage from '../components/add'
import EditPage from '../components/edit'

export default {
  name: page.name,
  components: { AddPage, EditPage },
  data () {
    return {
      curr: { id: '' },
      list: {
        title: page.title,
        cols,
        action: api.query,
        model: {
        }
      },
      removeAction: api.remove,
      dialog: {
        add: false,
        edit: false
      },
      buttons: page.buttons
    }
  },
  methods: {
    refresh () {
      this.$refs.list.refresh()
    },
    add () {
      this.dialog.add = true
    },
    edit (row) {
      this.curr = row
      this.dialog.edit = true
    }
  }
}
</script>
