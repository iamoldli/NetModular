<template>
  <nm-container>
    <nm-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-row :gutter="20">
          <el-col :span="11" :offset="1">
            <el-form-item label="名称：" prop="name">
              <el-input v-model="list.conditions.name" clearable/>
            </el-form-item>
          </el-col>
          <el-col :span="11">
            <el-form-item label="模块：" prop="moduleCode">
              <module-info-select v-model="list.conditions.moduleCode" @change="onModuleChange"/>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row :gutter="20">
          <el-col :span="11" :offset="1">
            <el-form-item label="控制器：" prop="controller">
              <nm-select ref="controllerSelect" :method="getAllControllerAction" v-model="list.conditions.controller" @change="onControllerChange">
                <template v-slot:default="{options}">
                  <el-option v-for="option in options" :key="option.value" :label="option.label" :value="option.value">
                    <span>{{option.label}}({{option.value}})</span>
                  </el-option>
                </template>
              </nm-select>
            </el-form-item>
          </el-col>
          <el-col :span="11">
            <el-form-item label="方法：" prop="action">
              <nm-select ref="actionSelect" :method="getAllAction" v-model="list.conditions.action">
                <template v-slot:default="{options}">
                  <el-option v-for="option in options" :key="option.value" :label="option.label" :value="option.value">
                    <span>{{option.label}}({{option.value}})</span>
                  </el-option>
                </template>
              </nm-select>
            </el-form-item>
          </el-col>
        </el-row>
      </template>

      <!--工具栏-->
      <template v-slot:toolbar>
        <nm-button :text="buttons.sync.text" :icon="buttons.sync.icon" @click="sync" v-nm-has="buttons.sync"/>
      </template>

      <template v-slot:col-moduleName="{row}">
        <span>{{`${row.moduleName}(${row.moduleCode})`}}</span>
      </template>

      <!--操作列-->
      <template v-slot:col-operation="{row}">
        <nm-button-delete :action="removeAction" :id="row.id" @success="refresh" v-nm-has="buttons.del"/>
      </template>
    </nm-list>
  </nm-container>
</template>
<script>
import api from '../../../api/permission'
import systemApi from '../../../api/system'
import page from './page'
import cols from './cols'
import ModuleInfoSelect from '../../moduleInfo/select/'

export default {
  name: page.name,
  components: { ModuleInfoSelect },
  data () {
    return {
      list: {
        title: page.title,
        cols,
        labelWidth: '70px',
        action: api.query,
        conditions: {
          moduleCode: '',
          name: '',
          controller: '',
          action: ''
        }
      },
      removeAction: api.remove,
      buttons: page.buttons
    }
  },
  methods: {
    refresh () {
      this.$refs.list.refresh()
    },
    sync () {
      this._confirm('您确认要同步权限信息吗', '同步权限信息').then(() => {
        api.sync().then(data => {
          this._success('同步成功')
          this.refresh()
        })
      })
    },
    getAllControllerAction () {
      return systemApi.getAllController({ module: this.list.conditions.moduleCode })
    },
    getAllAction () {
      const con = this.list.conditions
      return systemApi.getAllAction({ module: con.moduleCode, controller: con.controller })
    },
    onModuleChange () {
      this.$refs.controllerSelect.refresh()
    },
    onControllerChange () {
      this.$refs.actionSelect.refresh()
    }
  }
}
</script>
