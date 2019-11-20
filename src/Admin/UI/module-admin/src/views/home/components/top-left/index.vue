<template>
  <nm-box v-bind="box">
    <template v-slot:toolbar>
      <nm-button icon="refresh" @click="refresh" />
    </template>
    <ve-line :data="chartData"></ve-line>
  </nm-box>
</template>
<script>
// 接口
const api = $api.admin.auditInfo

export default {
  data() {
    return {
      box: {
        title: '最近一周审计日志数量',
        icon: 'chart-line',
        header: true,
        border: true
      },
      chartData: {
        columns: ['日期', '数量'],
        rows: []
      }
    }
  },
  methods: {
    async refresh() {
      this.chartData.rows = []
      const data = await api.queryLatestWeekPv()
      let rows = []
      data.map(m => {
        rows.push({ 日期: m.key, 数量: m.value })
      })
      this.chartData.rows = rows
    }
  },
  created() {
    this.refresh()
  }
}
</script>
