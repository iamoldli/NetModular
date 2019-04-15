<template>
  <nm-container>
    <nm-list title="菜单列表" icon="list" operation :search="search" :action="action" :conditions="conditions" :cols="cols">
      <template v-slot:toolbar>
        <nm-button icon="add" text="添加"/>
        <nm-button icon="delete" text="删除"/>
      </template>

      <template v-slot:querybar>
        <el-row>
          <el-col :span="10">
            <el-form-item label="名称：" prop="name">
              <el-input v-model="conditions.name" clearable/>
            </el-form-item>
          </el-col>
        </el-row>
      </template>

      <template v-slot:col-show="{row}">{{row.show?'是':'否'}}</template>

      <template v-slot:col-operation>
        <nm-button icon="edit" text="编辑"/>
        <nm-button icon="delete" text="删除"/>
      </template>
    </nm-list>
  </nm-container>
</template>
<script>
export default {
  data () {
    return {
      conditions: {
        name: ''
      },
      cols: [{
        name: 'id',
        label: '编号',
        show: false
      },
      {
        name: 'name',
        label: '名称',
        suggest: true,
        icon: 'list'
      },
      {
        name: 'code',
        label: '编码',
        suggest: true
      },
      {
        name: 'module',
        label: '模块'
      },
      {
        name: 'show',
        label: '是否显示'
      },
      {
        name: 'creator',
        label: '创建人'
      },
      {
        name: 'createdTime',
        label: '创建时间'
      }],
      search: {
        placeholder: '搜索：名称、编码'
      }
    }
  },
  methods: {
    action (model) {
      console.log(model)
      var template = { total: 1200 }
      template['rows|' + model.page.size] = [{
        'id': '@guid',
        'name': '@cname',
        'code': '@word(5,5)',
        'module': '权限管理',
        'show': '@boolean',
        'creator': '@cname',
        'createdTime': '@datetime()'
      }]
      let data = this.$mock(template)
      if (model.name) {
        data.rows = data.rows.filter(item => item.name.indexOf(model.name) > -1)
      }
      if (model.code) {
        data.rows = data.rows.filter(item => item.code.indexOf(model.code) > -1)
      }
      return new Promise(resolve => {
        resolve(data)
      })
    }
  }
}
</script>
