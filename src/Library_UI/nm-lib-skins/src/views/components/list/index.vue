<template>
  <nm-container>
    <nm-box page header title="列表页(nm-list)" icon="list">
      <nm-demo-desc>
        <p>列表页是一个页面级组件，用于列表的展示</p>
        <p>列表页组件会填充整个内容区域，当表格内容超出时会自动显示滚动条</p>
      </nm-demo-desc>

      <nm-table-attributes :data="attrs"/>
      <nm-table-methods :data="methods"/>
      <nm-table-events :data="events"/>
      <nm-table-slots :data="slots"/>

      <nm-demo :options="demo1">
        <demo1/>
      </nm-demo>
    </nm-box>
  </nm-container>
</template>
<script>
import attrs from './data/attrs.js'
import methods from './data/methods.js'
import events from './data/events.js'
import slots from './data/slots.js'
import demo1 from './demo/1'
export default {
  components: { demo1 },
  data () {
    return {
      attrs,
      methods,
      events,
      slots,
      demo1: {
        title: '列表页',
        height: 600,
        html: `  <nm-list title="用户列表" :action="query" :conditions="conditions" :cols="cols" @select="select">
    <template v-slot:querybar>
      <el-form-item label="姓名：" prop="name">
        <el-input v-model="conditions.name"/>
      </el-form-item>
      <el-form-item label="性别：" prop="sex">
        <el-select v-model="conditions.sex">
          <el-option label="男" value="0"></el-option>
          <el-option label="女" value="1"></el-option>
        </el-select>
      </el-form-item>
    </template>

    <template v-slot:toolbar="{rows}">
      <nm-button icon="add" circle type="success" @click="add"/>
      <nm-button icon="delete" circle type="danger" @click="remove(rows)"/>
    </template>

    <template v-slot:col-sex="{row}">
      <el-tag v-if="row.sex===1" type="success">男</el-tag>
      <el-tag v-else type="danger">女</el-tag>
    </template>
  </nm-list>`,
        js: `export default {
  data () {
    return {
      conditions: {
        name: '',
        sex: ''
      },
      cols: [{
        name: 'id',
        label: '序号'
      }, {
        name: 'name',
        label: '名称'
      }, {
        name: 'age',
        label: '年龄'
      }, {
        name: 'sex',
        label: '性别'
      }, {
        name: 'address',
        label: '地址'
      }, {
        name: 'zip',
        label: '邮编'
      }]
    }
  },
  methods: {
    query () {
      return new Promise((resolve, reject) => {
        const r = this.$random
        let data = this.$mock({
          'rows|15': [{
            'id|+1': 1,
            'name' () {
              return r.cname()
            },
            'age|20-30': 0,
            'sex|0-1': 0,
            'address' () {
              return r.county('@county(true)')
            },
            'zip' () {
              return r.zip()
            }
          }],
          'total': 500
        })
        resolve(data)
      })
    },
    add () {
      this._confirm('添加')
    },
    remove (rows) {
      if (!rows || rows.length < 1) {
        this._warning('请选择数据')
      } else {
        this._confirm('您选择了：' + rows.map(item => item.id + ':' + item.name))
      }
    }
  }
}`
      }
    }
  }
}
</script>
