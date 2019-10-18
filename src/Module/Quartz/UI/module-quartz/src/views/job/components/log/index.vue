<template>
  <nm-list-dialog v-bind="dialog" :visible.sync="visible_" @open="onOpen">
    <nm-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item label="开始日期：" prop="startDate">
          <el-date-picker v-model="list.model.startDate" type="date" placeholder="选择日期" value-format="yyyy-MM-dd"></el-date-picker>
        </el-form-item>
        <el-form-item label="结束日期：" prop="endDate">
          <el-date-picker v-model="list.model.endDate" type="date" placeholder="选择日期" value-format="yyyy-MM-dd"></el-date-picker>
        </el-form-item>
      </template>

      <template v-slot:col-type="{row}">
        <el-tag v-if="row.type===0" type="success" effect="dark" size="small">信息</el-tag>
        <el-tag v-else-if="row.type===1" type="warning" effect="dark" size="small">调试</el-tag>
        <el-tag v-else type="danger" effect="dark" size="small">异常</el-tag>
      </template>
    </nm-list>
  </nm-list-dialog>
</template>
<script>
import { mixins } from 'nm-lib-skins'
import cols from './cols'

const api = $api.quartz.job

export default {
  mixins: [mixins.dialog],
  data() {
    return {
      list: {
        noHeader: true,
        noOperation: true,
        cols,
        action: api.log,
        model: {
          jobId: '',
          startDate: '',
          endDate: ''
        }
      }
    }
  },
  props: {
    job: {
      type: Object
    }
  },
  computed: {
    dialog() {
      return {
        title: `任务日志(${this.job.name})`,
        icon: 'log'
      }
    }
  },
  methods: {
    onOpen() {
      this.list.model.jobId = this.job.id
    }
  }
}
</script>
