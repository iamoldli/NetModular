<template>
  <nm-container>
    <nm-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item label="名称：" prop="name">
          <el-input v-model="list.model.name" clearable />
        </el-form-item>
      </template>

      <!--高级查询-->
      <template v-slot:querybar-advanced>
        <el-row>
          <el-col :span="20" :offset="1">
            <el-form-item label="模块：" prop="moduleCode">
              <module-info-select v-model="list.model.moduleCode" @change="onModuleChange" />
            </el-form-item>
            <el-form-item label="控制器：" prop="controller">
              <nm-select ref="controllerSelect" :method="getAllControllerAction" v-model="list.model.controller" @change="onControllerChange">
                <template v-slot:default="{options}">
                  <el-option v-for="option in options" :key="option.value" :label="option.label" :value="option.value">
                    <span>{{option.label}}({{option.value}})</span>
                  </el-option>
                </template>
              </nm-select>
            </el-form-item>
            <el-form-item label="方法：" prop="action">
              <nm-select ref="actionSelect" :method="getAllAction" v-model="list.model.action">
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

      <!--按钮-->
      <template v-slot:querybar-buttons>
        <nm-button v-bind="buttons.sync" @click="sync" />
      </template>

      <template v-slot:col-moduleName="{row}">
        <span>{{`${row.moduleName}(${row.moduleCode})`}}</span>
      </template>
    </nm-list>
  </nm-container>
</template>
<script>
import page from './page'
import cols from './cols'
import ModuleInfoSelect from '../../moduleInfo/components/select'

// 接口
const api = $api.admin.permission
const systemApi = $api.admin.system

export default {
  name: page.name,
  components: { ModuleInfoSelect },
  data() {
    return {
      list: {
        title: page.title,
        cols,
        labelWidth: '70px',
        noOperation: true,
        action: api.query,
        advanced: {
          enabled: true,
          width: '400px'
        },
        model: {
          moduleCode: '',
          name: '',
          controller: '',
          action: ''
        },
        loading: false
      },
      buttons: page.buttons
    }
  },
  methods: {
    refresh() {
      this.$refs.list.refresh()
    },
    sync() {
      this._confirm('您确认要同步权限信息吗', '同步权限信息').then(() => {
        this.list.loading = true
        api.sync().then(data => {
          this.list.loading = false
          this._success('同步成功')
          this.refresh()
        })
      })
    },
    getAllControllerAction() {
      return systemApi.getAllController({ module: this.list.model.moduleCode })
    },
    getAllAction() {
      const con = this.list.model
      return systemApi.getAllAction({ module: con.moduleCode, controller: con.controller })
    },
    onModuleChange() {
      this.$refs.controllerSelect.refresh()
    },
    onControllerChange() {
      this.$refs.actionSelect.refresh()
    }
  }
}
</script>
