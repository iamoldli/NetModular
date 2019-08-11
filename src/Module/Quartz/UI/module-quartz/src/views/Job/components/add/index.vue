<template>
  <nm-form-dialog ref="form" v-bind="form" :rules="rules" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="10" :offset="1">
        <el-form-item label="任务分组：" prop="group">
          <group-select v-model="form.model.group" />
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="触发器类型：" prop="triggerType">
          <el-radio-group v-model="form.model.triggerType">
            <el-radio :label="0">简单触发器</el-radio>
            <el-radio :label="1">CRON触发器</el-radio>
          </el-radio-group>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="10" :offset="1">
        <el-form-item label="任务名称：" prop="name">
          <el-input v-model="form.model.name" />
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="任务编码：" prop="code">
          <el-input v-model="form.model.code" />
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="模块：" prop="moduleCode">
          <module-select v-model="form.model.moduleCode" />
        </el-form-item>
        <el-form-item label="任务类：" prop="jobClass">
          <job-select :module-id="form.model.moduleCode" v-model="form.model.jobClass">
            <template v-slot:default="{options}">
              <el-option v-for="item in options" :key="item.value" :label="`${item.label} (${item.value})`" :value="item.value"></el-option>
            </template>
          </job-select>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="10" :offset="1">
        <el-form-item label="开始日期：" prop="beginDate">
          <el-date-picker v-model="form.model.beginDate" type="date" placeholder="选择日期" value-format="yyyy-MM-dd"></el-date-picker>
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="结束日期：" prop="endDate">
          <el-date-picker v-model="form.model.endDate" type="date" placeholder="选择日期" value-format="yyyy-MM-dd"></el-date-picker>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row v-if="form.model.triggerType===0">
      <el-col :span="10" :offset="1">
        <el-form-item label="间隔时间：" prop="interval">
          <el-input v-model.number="form.model.interval" placeholder="请输入执行时间间隔">
            <template slot="append">秒</template>
          </el-input>
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="重复次数：" prop="repeatCount">
          <el-input v-model.number="form.model.repeatCount" placeholder="请输入重复次数，0表示无限次">
            <template slot="append">次</template>
          </el-input>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row v-else>
      <el-col :span="20" :offset="1">
        <el-form-item label="CRON表达式：" prop="cron">
          <el-input v-model="form.model.cron" placeholder="请输入CRON表达式"></el-input>
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-dialog>
</template>
<script>
import { mixins } from 'nm-lib-skins'
import api from '../../../../api/Job'
import ModuleSelect from '../module-select'
import JobSelect from '../job-select'
import GroupSelect from '../../../Group/components/select'

export default {
  mixins: [mixins.dialog],
  components: { ModuleSelect, JobSelect, GroupSelect },
  data() {
    return {
      form: {
        title: '添加任务',
        icon: 'add',
        width: '900px',
        labelWidth: '120px',
        action: api.add,
        model: {
          moduleCode: '',
          jobClass: '',
          group: '',
          name: '',
          code: '',
          triggerType: 0,
          interval: 5,
          repeatCount: 0,
          cron: '',
          beginDate: '',
          endDate: ''
        }
      },
      on: {
        success: this.onSuccess,
        open: this.onOpen
      }
    }
  },
  computed: {
    rules() {
      let rules = {
        moduleCode: [{ required: true, message: '请选择模块', trigger: 'change' }],
        jobClass: [{ required: true, message: '请选择任务类', trigger: 'change' }],
        group: [{ required: true, message: '请选择任务组', trigger: 'change' }],
        name: [{ required: true, message: '请输入任务名称', trigger: 'blur' }],
        code: [{ required: true, message: '请输入任务编码', trigger: 'blur' }],
        beginDate: [{ required: true, message: '请选择开始日期', trigger: 'change' }],
        endDate: [{ required: true, message: '请选择开始日期', trigger: 'change' }]
      }

      if (this.form.model.triggerType === 0) {
        rules['interval'] = [{ required: true, message: '请输入执行间隔时间', trigger: 'blur' }, { type: 'integer', min: 1, message: '请输入整数且不能小于1', trigger: 'blur' }]
        rules['repeatCount'] = [{ required: true, message: '请输入执行次数', trigger: 'blur' }, { type: 'integer', min: 0, message: '请输入整数且不能小于0', trigger: 'blur' }]
      } else {
        rules['cron'] = [{ required: true, message: '请输入CRON表达式', trigger: 'blur' }]
      }
      return rules
    }
  },
  methods: {
    onSuccess() {
      this.$emit('success')
    },
    onOpen() {
      this.$nextTick(() => {
        this.$refs.form.reset()
      })
    }
  }
}
</script>
