<template>
  <div>
    <el-row :gutter="20">
      <el-col :span="12">
        <section style="height:400px;background-color:#fff">
          <nm-scrollbar>
            <p v-for="i in 100" :key="i">第{{i}}个</p>
          </nm-scrollbar>
        </section>
      </el-col>
      <el-col :span="12">
        <section style="height:400px;background-color:#fff">
          <nm-scrollbar horizontal>
            <p v-for="i in 100" :key="i">
              <span v-for="m in i" :key="i+''+m">hahaah</span>
            </p>
          </nm-scrollbar>
        </section>
      </el-col>
    </el-row>
  </div>
</template>
<script>
export default {
  data() {
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
    query() {
      const rows = [{ id: 1, name: '张三', age: 22 }, { id: 2, name: '李四', age: 26 }, { id: 3, name: '王五', age: 26 }]
      return new Promise(resolve => {
        resolve({
          rows,
          total: 3
        })
      })
    },
    onSelect(row) {
      this.listbox.push({
        label: row.name,
        value: row.id
      })
    }
  }
}
</script>
