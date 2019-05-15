<template>
  <nm-container :scrollbar="true">
    <nm-query :option="tableOption" :page="page" :condition="condition" @query="query" @rowButton="rowButton" @button="topButton"></nm-query>
  </nm-container>
</template>
<script>
import xz from './xingzhengquhua'
export default {
  data() {
    return {
      tableOption:
      {

        search: [],
        buttons: [], // 顶部button
        coloumns: [],
        rows: []
      },
      page:
      {
        index: 1, // 当前页
        total: 200, // 数据总数
        pageSize: 5 // 每页显示数量
      },
      condition: { Province: '593f9c2ccf33985daebb1355' } // 条件
    }
  },
  computed: {
    getProvince: function () {
      return this.condition['Province']
    },
    city: function () {
      return this.condition['city']
    }
  },
  watch: {
    getProvince(val) {
      delete this.condition['city']
      delete this.condition['area']
      this.tableOption.search.items[8].options.length = 0
      this.tableOption.search.items[7].options = xz.filter(o => o.parent === val).map(function (o) {
        var rObj = {}
        rObj['key'] = o._id
        rObj['value'] = o.name
        return rObj
      })
    },
    city(val) {
      delete this.condition['area']
      this.tableOption.search.items[8].options = xz.filter(o => o.parent === val).map(function (o) {
        var rObj = {}
        rObj['key'] = o._id
        rObj['value'] = o.name
        return rObj
      })
    }

  },
  created() {
    this.tableOption.search = {      'show': false,
      'items': [
        {
          'key': 'sex',
          'label': '性别',
          'type': 'select',
          'placeholder': '请选择',
          'options': [{ key: 0, value: '分组', options: [{ key: 0, value: '男' }, { key: 1, value: '女' }] }, { key: 0, value: '其他', options: [{ key: 2, value: '妖妖' }] }]
        },
        {
          'key': 'beginTime',
          'label': '开始日期',
          'type': 'date',
          'format': 'yyyy-MM-dd',
          'placeholder': '请选择开始日期'
        },
        {
          'key': 'name',
          'label': '姓名',
          'type': 'normal',
          'placeholder': '请输入姓名'
        },
        {          'key': 'age',
          'type': 'normal',
          'label': '年龄',
          'placeholder': '请输入年龄'
        },
        {          'key': 'address',
          'type': 'normal',
          'label': '地址',
          'placeholder': '请输入地址'
        },
        {          'key': 'zip',
          'type': 'normal',
          'label': '邮编',
          'placeholder': '请输入邮编'
        },
        {          'key': 'Province',
          'type': 'select',
          'label': '省份',
          'placeholder': '请选择',
          'options': []
        },
        {          'key': 'city',
          'type': 'select',
          'label': '市',
          'placeholder': '请选择',
          'options': []
        },
        {          'key': 'area',
          'type': 'select',
          'label': '区',
          'placeholder': '请选择',
          'options': []
        }
      ]    }
    this.tableOption.coloumns = [
      {        'key': 'id',
        'type': 'checkbox',
        'label': '序号',
        'options': ''
      },
      {        'key': 'id',
        'type': 'normal',
        'label': '序号',
        'options': ''
      },
      {        'key': 'name',
        'sortable': true,
        'type': 'normal',
        'label': '姓名',
        'options': ''
      },
      {        'key': 'sex',
        'type': 'select',
        'label': '性别',
        'options': [{ key: 0, value: '男' }, { key: 1, value: '女' }]
      },
      {        'key': 'age',
        'type': 'normal',
        'label': '年龄',
        'options': ''
      },
      {        'key': 'address',
        'type': 'normal',
        'label': '地址',
        'options': ''
      },
      {        'key': 'zip',
        'type': 'normal',
        'label': '邮编',
        'options': ''
      },
      {        'key': 'operation',
        'width': '300px',
        'type': 'operation',
        'showType': 'button', // button or text
        'label': '操作',
        'options': ''
      }

    ]
    this.tableOption.buttons = [
      { key: 'add', value: '新增' },
      { key: 'eidt', value: '修改' },
      { key: 'del', value: '删除' },
      { key: 'select', value: '查看' }
    ]
    // 初始化省数据
    this.tableOption.search.items[6].options = xz.filter(o => o.level === 0).map(function (o) {
      var rObj = {}
      rObj['key'] = o._id
      rObj['value'] = o.name
      return rObj
    })
    this.tableOption.search.items[7].options = xz.filter(o => o.parent === this.condition['Province']).map(function (o) {
      var rObj = {}
      rObj['key'] = o._id
      rObj['value'] = o.name
      return rObj
    })
    this.getData()
  },
  methods:
  {
    query(val) {
      this.getData()
    },
    rowButton(obj) {
    },
    topButton(obj) {
    },
    getData() {
      let random = this.$random
      var self = this
      setTimeout(function () {
        let o = {
          'id|+1': ((self.page.index - 1) * self.page.pageSize + 1),
          'name'() {
            return random.cname()
          },
          'age|20-30': 0,
          'sex|0-1': 0,
          'address'() {
            return random.county('@county(true)')
          },
          'zip'() {
            return random.zip()
          },
          'operation':
            [{ key: 'add', value: '新增' },
            { key: 'eidt', value: '修改' },
            { key: 'del', value: '删除' },
            { key: 'select', value: '查看' }]
        }
        let p = {}
        p['rows|' + self.page.pageSize] = [o]
        self.tableOption.rows = self.$mock(p).rows
      }, 200)
    }

  }
}
</script>
