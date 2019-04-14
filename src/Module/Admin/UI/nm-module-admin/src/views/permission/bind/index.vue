<template>
  <nm-split v-model="split" class="permission-bind" :loading="loading">
    <template v-slot:left>
      <nm-listbox title="已选列表" v-model="selection"/>
    </template>
    <template v-slot:right>
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

        <!--操作列-->
        <template v-slot:col-operation="{row}">
          <nm-button v-if="notIn(row)" icon="add" type="success" circle @click="add(row)"/>
          <el-tag v-else type="warning" disable-transitions>已选</el-tag>
        </template>

        <template v-slot:footer>
          <nm-button type="success" text="保存" @click="save"/>
        </template>
      </nm-list>
    </template>
  </nm-split>
</template>
<script>
import api from '../../../api/permission'
import systemApi from '../../../api/system'
import ModuleInfoSelect from '../../moduleInfo/select/'
export default {
  components: { ModuleInfoSelect },
  data () {
    return {
      loading: false,
      split: 0.2,
      list: {
        title: '权限列表',
        footerReverse: true,
        noSelectColumn: true,
        labelWidth: '70px',
        action: api.query,
        conditions: {
          moduleCode: '',
          name: '',
          controller: '',
          action: ''
        },
        cols: [
          {
            name: 'moduleName',
            label: '所属模块',
            width: 120
          },
          {
            name: 'name',
            label: '名称',
            width: 300,
            suggest: true
          },
          {
            name: 'controller',
            label: '控制器'
          },
          {
            name: 'action',
            label: '方法'
          }
        ]
      },
      // 已选列表
      selection: []
    }
  },
  props: {
    query: Function,
    action: Function
  },
  computed: {
    permissionList () {
      if (!this.selection) return []
      return this.selection.map(item => item.value)
    }
  },
  methods: {
    add (row) {
      if (this.selection.every(item => item.value !== row.id)) {
        this.selection.push({ label: row.name, value: row.id })
      }
    },
    notIn (row) {
      row.hasIn = !this.selection.every(item => item.value !== row.id)
      return !row.hasIn
    },
    save () {
      this.loading = true
      this.action(this.permissionList).then(data => {
        this._success('绑定成功')
        this.loading = false
        this.$emit('success')
      }).catch(() => {
        this.loading = false
      })
    },
    refresh () {
      this.querySelection()
      this.$refs.list.refresh()
    },
    // 查询已绑定的权限列表
    querySelection () {
      this.selection = []
      this.loading = true
      this.query().then(data => {
        data.forEach(element => {
          this.selection.push({ label: element.name, value: element.id })
        })
        this.loading = false
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
