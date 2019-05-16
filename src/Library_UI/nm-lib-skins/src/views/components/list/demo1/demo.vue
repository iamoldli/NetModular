<template>
  <nm-list v-bind="list">
    <template v-slot:querybar>
      <el-form-item label="姓名：" prop="name">
        <el-input v-model="list.model.name"/>
      </el-form-item>
    </template>

    <template v-slot:col-operation>
      <nm-button type="text" text="编辑" icon="edit"/>
      <nm-button-delete type="text" :action="remove" id="1" @success="onSuccess"/>
    </template>
  </nm-list>
</template>
<script>
export default {
  data () {
    return {
      list: {
        title: '列表页示例',
        icon: 'list',
        action: this.query,
        model: {
          name: ''
        },
        cols: [{
          name: 'id',
          label: '编号',
          show: false
        },
        {
          name: 'name',
          label: '姓名'
        },
        {
          name: 'age',
          label: '年龄'
        }
        ]
      }
    }
  },
  methods: {
    query () {
      const rows = [{ id: 1, name: '张三', age: 22 }, { id: 2, name: '李四', age: 26 }, { id: 3, name: '王五', age: 26 }]
      return new Promise(resolve => {
        resolve({
          rows,
          total: 3
        })
      })
    },
    remove () { },
    onSuccess () {
      this.query()
    }
  }
}
</script>
