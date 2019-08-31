<template>
  <nm-box header title="柱状图" icon="chart-bar">
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
      <nm-button icon="refresh" />
    </template>
    <div ref="chart" style="width:100%;height:100%"></div>
  </nm-box>
</template>
<script>
export default {
  data() {
    return {
      chart: null,
      date: [],
      xData: ['芝罘区', '福山区', '莱山区', '牟平区', '海阳市', '莱阳市', '蓬莱市', '栖霞市', '龙口市', '长岛县', '招远市', '莱州市', '开发区', '高新区', '昆嵛山', '龙海', '机场', '核电'],
      y1Data: [10758, 3975, 2980, 1831, 2122, 3018, 3525, 1729, 2764, 380, 3988, 2333, 3596, 453, 365, 9, 67, 48],
      y2Data: [28, 4.4, 5.9, 0.9, 0.7, 1.1, 2.1, 0.4, 1.9, 3.8, 1.6, 0.7, 5.7, 6.1]
    }
  },
  computed: {
    options() {
      return {
        grid: {
          left: '5%',
          right: '5%',
          top: '20%',
          bottom: '10%'
        },
        title: {
          show: false
        },
        tooltip: {
          trigger: 'axis',
          axisPointer: {
            type: 'shadow'
          }
        },
        legend: {
          data: ['监控数量', '监控密度'],
          right: '3%',
          top: '5%',
          itemWidth: 11,
          itemHeight: 11,
          textStyle: {
            fontSize: 13
          }
        },
        toolbox: {
          show: false
        },
        xAxis: [
          {
            type: 'category',
            boundaryGap: true,
            show: true,
            axisTick: {
              show: false
            },
            axisLabel: {
              fontSize: 15,
              margin: 8,
              interval: 0,
              formatter: function (val) {
                return val.split('').join('\n')
              }
            },
            axisLine: {
              lineStyle: {
                type: 'solid',
                color: '#4e608b',
                width: '1'
              }
            },
            data: this.xData
          }
        ],
        yAxis: [
          {
            type: 'value',
            scale: true,
            name: '',
            axisLine: {
              show: false
            },
            splitNumber: 4,
            axisTick: {
              show: false
            },
            splitLine: {
              lineStyle: {
                // 使用深浅的间隔色
                color: '#4e608b'
              }
            },
            axisLabel: {
              fontSize: 13,
              margin: 12
            },
            max: 12000,
            min: 0,
            boundaryGap: [0.2, 0.2]
          },
          {
            type: 'value',
            scale: true,
            axisLine: {
              show: false
            },
            splitNumber: 3,
            axisTick: {
              show: false
            },
            axisLabel: {
              fontSize: 13,
              margin: 12
            },
            splitLine: {
              lineStyle: {
                // 使用深浅的间隔色
                color: '#4e608b'
              }
            },
            name: '',
            max: 30,
            min: 0,
            boundaryGap: [0.2, 0.2]
          }

        ],
        series: [
          {
            name: '监控数量',
            type: 'bar',
            label: {
              normal: {
                show: true,
                position: 'top',
                textStyle: {
                  color: '#1dacfe'
                }
              }
            },
            itemStyle: {
              normal: {
                color: new this.$echarts.graphic.LinearGradient(0, 1, 0, 0, [{
                  offset: 0,
                  color: '#4889fb' // 0% 处的颜色
                }, {
                  offset: 1,
                  color: '#15b3ff' // 100% 处的颜色
                }], false)
              }
            },
            barWidth: '40%',
            yAxisIndex: 0,
            data: this.y1Data
          },
          {
            name: '监控密度',
            yAxisIndex: 1,
            color: '#ffd300',
            label: {
              normal: {
                show: true,
                position: 'top',
                textStyle: {
                  color: '#ffd300'
                }
              }
            },
            lineStyle: {
              color: '#ffd300'
            },
            type: 'line',
            data: this.y2Data
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
