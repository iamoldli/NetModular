<template>
  <nm-split v-model="split">
    <template v-slot:left>
      <nm-listbox title="已选用户" v-model="listbox"></nm-listbox>
    </template>
    <template v-slot:right>
      <nm-list v-bind="list">
        <template v-slot:querybar>
          <el-form-item label="姓名：" prop="name">
            <el-input v-model="list.model.name"/>
          </el-form-item>
        </template>

        <template v-slot:col-operation="{row}">
          <nm-button type="text" text="选择" @click="onSelect(row)"/>
        </template>
      </nm-list>
    </template>
  </nm-split>
</template>
<script>
export default {
  data () {
    return {
      split: 0.3,
      listbox: [],
      list: {
        title: '用户列表',
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
    onSelect (row) {
      this.listbox.push({
        label: row.name,
        value: row.id
      })
    }
  }
}
</script>
