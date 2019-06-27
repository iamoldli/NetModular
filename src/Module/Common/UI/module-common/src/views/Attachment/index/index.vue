<template>
  <nm-container>
    <nm-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item label="名称：" prop="name">
          <el-input v-model="list.model.name" clearable/>
        </el-form-item>
      </template>

      <!--操作列-->
      <template v-slot:col-operation="{row}">
        <nm-button type="text" :text="buttons.export.text" :icon="buttons.export.icon" @click="onExport(row)" v-nm-has="buttons.export"/>
        <nm-button-delete :id="row.id" :action="removeAction" @success="refresh" v-nm-has="buttons.del"/>
      </template>
    </nm-list>
  </nm-container>
</template>
<script>
import api from '../../../api/Attachment'
import page from './page'
import cols from './cols'
export default {
  name: page.name,
  data() {
    return {
      curr: { id: '' },
      list: {
        title: page.title,
        operationWidth: '150',
        cols,
        action: api.query,
        model: {
          /** 名称 */
          name: ''
        }
      },
      removeAction: api.remove,
      buttons: page.buttons,
      dialog: {
        upload: false
      }
    }
  },
  methods: {
    refresh() {
      this.$refs.list.refresh()
    },
    upload() {
      this.dialog.upload = true
    },
    onExport(row) {
      api.exportFile(row.id)
    }
  }
}
</script>
