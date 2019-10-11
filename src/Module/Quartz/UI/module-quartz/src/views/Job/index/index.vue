<template>
  <nm-container>
    <nm-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item label="名称：" prop="name">
          <el-input v-model="list.model.name" clearable />
        </el-form-item>
      </template>

      <!--按钮-->
      <template v-slot:querybar-buttons>
        <nm-button-has :options="buttons.add" @click="add" />
      </template>

      <template v-slot:col-jobClass="{row}">{{row.jobClass.split(',')[0]}}</template>

      <template v-slot:col-triggerType="{row}">
        <el-tag v-if="row.triggerType===0" size="small">简单</el-tag>
        <el-tag v-else type="warning" size="small">CRON</el-tag>
      </template>

      <template v-slot:col-interval="{row}">
        <span v-if="row.triggerType===0">{{row.interval}}秒</span>
        <span v-else>-</span>
      </template>

      <template v-slot:col-repeatCount="{row}">
        <span v-if="row.triggerType===1">-</span>
        <span v-else-if="row.repeatCount===0">无限次</span>
        <span v-else>{{row.repeatCount}}次</span>
      </template>

      <template v-slot:col-cron="{row}">
        <span v-if="row.triggerType===0">-</span>
        <span v-else>{{row.cron}}</span>
      </template>

      <template v-slot:col-status="{row}">
        <el-tag v-if="row.status===0" type="success" effect="dark" size="small">运行中</el-tag>
        <el-tag v-else-if="row.status===1" type="warning" effect="dark" size="small">暂停</el-tag>
        <el-tag v-else-if="row.status===2" type="primary" effect="dark" size="small">已完成</el-tag>
        <el-tag v-else type="danger" effect="dark" size="small">异常</el-tag>
      </template>

      <!--操作列-->
      <template v-slot:col-operation="{row}">
        <nm-button-has :options="buttons.edit" @click="edit(row)" />
        <nm-button-has v-if="row.status===0" :options="buttons.pause" @click="pause(row)" />
        <nm-button-has v-else :options="buttons.resume" @click="resume(row)" />
        <nm-button-has :options="buttons.log" @click="log(row)" />
        <nm-button-delete :options="buttons.del" :id="row.id" :action="removeAction" @success="refresh" />
      </template>
    </nm-list>

    <!--添加-->
    <add-page :visible.sync="dialog.add" @success="refresh" />
    <!--编辑-->
    <edit-page :id="curr.id" :visible.sync="dialog.edit" @success="refresh" />
    <!--日志-->
    <job-log :job="curr" :visible.sync="dialog.log" />
  </nm-container>
</template>
<script>
import page from './page'
import cols from './cols'
import AddPage from '../components/add'
import EditPage from '../components/edit'
import JobLog from '../components/log'

const api = $api.quartz.job

export default {
  name: page.name,
  components: { AddPage, JobLog, EditPage },
  data() {
    return {
      curr: { id: '', name: '' },
      list: {
        title: page.title,
        cols,
        operationWidth: '230',
        action: api.query,
        model: {
          name: ''
        }
      },
      removeAction: api.remove,
      dialog: {
        add: false,
        edit: false,
        log: false
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
    pause(row) {
      this._confirm(`您确定要暂停任务(${row.name})吗?`, '提醒').then(() => {
        api.pause(row.id).then(() => {
          row.status = 1
        })
      })
    },
    resume(row) {
      this._confirm(`您确定要暂停启动(${row.name})吗?`, '提醒').then(() => {
        api.resume(row.id).then(() => {
          row.status = 0
        })
      })
    },
    log(row) {
      this.curr = row
      this.dialog.log = true
    }
  }
}
</script>
