<template>
  <nm-split v-model="split" class="permission-bind" :loading="loading">
    <template v-slot:left>
      <nm-listbox title="已选列表" v-model="selection"/>
    </template>
    <template v-slot:right>
      <nm-list ref="list" v-bind="list">
        <!--查询条件-->
        <template v-slot:querybar>
          <el-form-item label="模块：" prop="moduleCode">
            <module-info-select v-model="list.conditions.moduleCode"/>
          </el-form-item>
          <el-form-item label="名称：" prop="name">
            <el-input v-model="list.conditions.name" clearable/>
          </el-form-item>
          <el-form-item label="控制器：" prop="controller">
            <el-input v-model="list.conditions.controller" clearable/>
          </el-form-item>
          <el-form-item label="方法：" prop="action">
            <el-input v-model="list.conditions.action" clearable/>
          </el-form-item>
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
        search: { advanced: { height: '300px' } },
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
            width: 150,
            suggest: true
          },
          {
            name: 'controller',
            label: '控制器',
            suggest: true
          },
          {
            name: 'action',
            label: '方法'
          }
        ]
      },
      listAction: api.query,
      // 已选列表
      selection: []

    }
  },
  props: {
    id: [String, Number],
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
      this.action({ id: this.id, permissionList: this.permissionList }).then(data => {
        this._success('绑定成功')
        this.loading = false
        this.$emit('success')
      }).catch(() => {
        this.loading = false
      })
    },
    // 查询已绑定的权限列表
    querySelection () {
      this.selection = []
      if (this.id) {
        this.loading = true
        this.query(this.id).then(data => {
          data.forEach(element => {
            this.selection.push({ label: element.name, value: element.id })
          })
          this.loading = false
        })
      }
    }
  },
  created () {
    this.querySelection()
  },
  watch: {
    id () {
      this.querySelection()
    }
  }
}
</script>
