<template>
  <nm-box header title="饼图" icon="chart-pie">
    <template v-slot:toolbar-before>
      <el-date-picker
        size="mini"
        style="width: 221px !important;"
        v-model="date"
        type="daterange"
        align="right"
        valueFormat="yyyy-MM-dd"
        range-separator="至"
        start-placeholder="开始日期"
        end-placeholder="结束日期"
      ></el-date-picker>
    </template>
    <template v-slot:toolbar>
      <nm-button icon="refresh"/>
    </template>
    <div ref="chart" style="width:100%;height:100%"></div>
  </nm-box>
</template>
<script>
export default {
  data() {
    return {
      chart: null,
      date: []
    }
  },
  computed: {
    options() {
      return {
        //          title: {
        //              text: 'ECharts 入门示例'
        //          },
        tooltip: {},
        legend: {
          data: ['衬衫', '羊毛衫', '雪纺衫', '裤子', '高跟鞋', '袜子']
        },
        xAxis: {
          data: ['衬衫', '羊毛衫', '雪纺衫', '裤子', '高跟鞋', '袜子']
        },
        yAxis: {},
        series: [
          {
            name: '衬衫',
            type: 'bar',
            barGap: '-100%',
            itemStyle: {
              normal: {
                color: '#C1232B'
              }
            },
            //              data: [5, 20, 36, 10, 10, 20]
            data: [5, 0, 0, 0, 0, 0]
          },
          {
            name: '羊毛衫',
            type: 'bar',
            barGap: '-100%',
            itemStyle: {
              normal: {
                color: '#B5C334'
              }
            },
            //              data: [5, 20, 36, 10, 10, 20]
            data: [0, 20, 0, 0, 0, 0]
          },
          {
            name: '雪纺衫',
            type: 'bar',
            barGap: '-100%',
            itemStyle: {
              normal: {
                color: '#FCCE10'
              }
            },
            //              data: [5, 20, 36, 10, 10, 20]
            data: [0, 0, 36, 0, 0, 0]
          },
          {
            name: '裤子',
            type: 'bar',
            barGap: '-100%',
            itemStyle: {
              normal: {
                color: '#E87C25'
              }
            },
            //              data: [5, 20, 36, 10, 10, 20]
            data: [0, 0, 0, 10, 0, 0]
          },
          {
            name: '高跟鞋',
            type: 'bar',
            barGap: '-100%',
            itemStyle: {
              normal: {
                color: '#27727B'
              }
            },
            //              data: [5, 20, 36, 10, 10, 20]
            data: [0, 0, 0, 0, 10, 0]
          },
          {
            name: '袜子',
            type: 'bar',
            barGap: '-100%',
            itemStyle: {
              normal: {
                color: '#FE8463'
              }
            },
            //              data: [5, 20, 36, 10, 10, 20]
            data: [0, 0, 0, 0, 0, 20]
          },
          {
            name: '访问来源',
            type: 'pie',
            radius: '28%',
            center: ['75%', '35%'],
            //          data:['衬衫','羊毛衫','雪纺衫','裤子','高跟鞋','袜子']
            //          data: [5, 20, 36, 10, 10, 20]

            data: [
              { value: 5, name: '衬衫', itemStyle: { color: '#C1232B' } },
              { value: 20, name: '羊毛衫', itemStyle: { color: '#B5C334' } },
              { value: 36, name: '雪纺衫', itemStyle: { color: '#FCCE10' } },
              { value: 10, name: '裤子', itemStyle: { color: '#E87C25' } },
              { value: 10, name: '高跟鞋', itemStyle: { color: '#27727B' } },
              { value: 20, name: '袜子', itemStyle: { color: '#FE8463' } }
            ],
            itemStyle: {
              emphasis: {
                shadowBlur: 10,
                shadowOffsetX: 0,
                shadowColor: 'rgba(0, 0, 0, 0.5)'
              }
            }
          }
        ]
      }
    }
  },
  methods: {
    init() {
      let chart = this.$echarts.init(this.$refs.chart)
      this.chart = chart
      this.chart.setOption(this.options)
      window.addEventListener('resize', function () {
        chart.resize()
      })
    }
  },
  mounted() {
    this.init()
  }
}
</script>
