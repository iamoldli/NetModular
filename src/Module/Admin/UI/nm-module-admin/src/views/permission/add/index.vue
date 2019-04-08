<template>
  <nm-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="2">
        <el-form-item label="模块" prop="moduleCode">
          <module-info-select v-model="form.model.moduleCode" @change="onModuleChange"/>
        </el-form-item>
        <el-form-item label="控制器" prop="controller">
          <nm-select ref="controllerSelect" :method="queryControllers" @change="onControllerChange" v-model="form.model.controller">
            <template v-slot:default="{options}">
              <el-option v-for="item in options" :key="item.value" :label="item.label" :value="item.value">
                <span style="float: left">{{item.label}}</span>
                <span style="float: right; color: #8492a6; font-size: 13px">{{item.data}}</span>
              </el-option>
            </template>
          </nm-select>
        </el-form-item>
        <el-form-item label="方法" prop="actions">
          <nm-checkbox-group ref="actionCheckbox" :action="queryActions" @change="onActionChange">
            <template v-slot:default="{option}">
              <span>{{`${option.label}(${option.value})`}}</span>
            </template>
          </nm-checkbox-group>
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-dialog>
</template>
<script>
import api from '../../../api/permission'
import { mixins } from 'nm-lib-skins'
import ModuleInfoSelect from '../../moduleInfo/select/'
export default {
  components: { ModuleInfoSelect },
  mixins: [mixins.dialog],
  data () {
    return {
      form: {
        title: '添加权限',
        icon: 'add',
        width: '700px',
        action: api.add,
        model: {
          moduleCode: '',
          controller: '',
          controllerName: '',
          actions: []
        },
        rules: {
          moduleCode: [
            { required: true, message: '请选择模块' }
          ],
          controller: [
            { required: true, message: '请选择控制器' }
          ],
          actions: [
            { required: true, message: '请选择方法' }
          ]
        }
      },
      on: {
        success: this.onSuccess,
        open: this.onOpen
      }
    }
  },
  methods: {
    // 查询控制器列表
    queryControllers () {
      if (this.form.model.moduleCode) {
        return api.controllers(this.form.model.moduleCode)
      } else {
        return new Promise(resolve => {
          resolve([])
        })
      }
    },
    // 查询方法列表
    queryActions () {
      if (this.form.model.controller) {
        return api.actions({ moduleCode: this.form.model.moduleCode, controller: this.form.model.controller })
      } else {
        return new Promise(resolve => {
          resolve([])
        })
      }
    },
    onModuleChange () {
      this.refreshController()
    },
    onControllerChange (val, selection, options) {
      if (selection) {
        this.form.model.controllerName = selection.label
      }
      this.refreshAction()
    },
    onActionChange (val, selection) {
      this.form.model.actions = []
      selection.map(item => {
        this.form.model.actions.push({ key: item.value, value: item.label })
      })
    },
    onSuccess () {
      this.$emit('success')
    },
    onOpen () {
      this.$nextTick(() => {
        this.$refs.form.reset()
        this.refreshController()
      })
    },
    refreshController () {
      this.form.model.controller = ''
      this.$refs.controllerSelect.refresh()
    },
    refreshAction () {
      this.form.model.actions = []
      this.$refs.actionCheckbox.clear()
      this.$refs.actionCheckbox.refresh()
    }
  }
}
</script>
