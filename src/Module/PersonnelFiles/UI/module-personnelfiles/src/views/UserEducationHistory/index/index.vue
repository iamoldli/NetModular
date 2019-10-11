<template>
  <nm-list-dialog v-bind="dialog_" @open="onOpen" :visible.sync="visible_">
    <nm-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item label="院校名称：" prop="schoolName">
          <el-input v-model="list.model.schoolName" clearable />
        </el-form-item>
      </template>

      <!--按钮-->
      <template v-slot:querybar-buttons>
        <nm-button type="success" text="添加" icon="add" @click="add" />
      </template>

      <template v-slot:col-date="{row}">{{$dayjs(row.startDate).format('YYYY-MM')}} ~ {{$dayjs(row.endDate).format('YYYY-MM')}}</template>

      <!--操作列-->
      <template v-slot:col-operation="{row}">
        <nm-button text="编辑" icon="edit" type="text" @click="edit(row)" />
        <nm-button-delete :id="row.id" :action="removeAction" @success="refresh" />
      </template>

      <!--添加-->
      <add-page :user-id="user.id" :visible.sync="dialog.add" @success="refresh" />
      <!--编辑-->
      <edit-page :id="curr.id" :visible.sync="dialog.edit" @success="refresh" />
    </nm-list>
  </nm-list-dialog>
</template>
<script>
import { mixins } from 'nm-lib-skins'
import cols from './cols'
import AddPage from '../components/add'
import EditPage from '../components/edit'

const api = $api.personnelFiles.userEducationHistory

export default {
  mixins: [mixins.dialog],
  components: { AddPage, EditPage },
  data() {
    return {
      curr: { id: '' },
      list: {
        noHeader: true,
        cols,
        action: api.query,
        model: {
          userId: '',
          schoolName: ''
        }
      },
      removeAction: api.remove,
      dialog: {
        add: false,
        edit: false
      }
    }
  },
  props: {
    user: {
      type: Object,
      required: true
    }
  },
  computed: {
    dialog_() {
      return {
        title: `教育经历--${this.user.name}`,
        icon: 'education',
        width: '70%',
        height: '80%'
      }
    }
  },
  methods: {
    refresh() {
      this.$nextTick(() => {
        this.$refs.list.refresh()
      })
    },
    add() {
      this.dialog.add = true
    },
    edit(row) {
      this.curr = row
      this.dialog.edit = true
    },
    onOpen() {
      const model = this.list.model
      if (model.userId !== this.user.id) {
        model.userId = this.user.id
        model.schoolName = ''
        this.refresh()
      }
    }
  }
}
</script>
