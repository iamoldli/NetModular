<template>
  <section :class="class_" v-loading="loading" :element-loading-text="loadingText" :element-loading-background="loadingBackground" :element-loading-spinner="loadingSpinner">
    <!--header-->
    <query-header v-if="!noHeader" :title="title" :icon="icon" :no-fullscreen="noFullscreen" :fullscreen.sync="fullscreen" :no-refresh="noRefresh">
      <template v-slot:toolbar>
        <slot name="header-toolbar" :total="total" :selection="selection"/>
      </template>
    </query-header>

    <!--工具栏-->
    <query-toolbar v-if="!noToolbar" v-model="filter" :search="search" :columns="columns">
      <template v-slot:toolbar>
        <slot name="toolbar" :total="total" :selection="selection"/>
      </template>

      <!--高级查询条件-->
      <template v-slot:querybar>
        <nm-form ref="queryForm" :model="conditions" :rules="conditionsRules" :label-width="labelWidth">
          <slot name="querybar"/>
        </nm-form>
      </template>
    </query-toolbar>

    <section class="nm-list-body">
      <query-table ref="table" :rows="rows" :cols="cols" :span-method="spanMethod" :selection.sync="selection">
        <!-- 多选 -->
        <el-table-column v-if="multiple" fixed="left" align="center" type="selection" width="55"/>

        <!-- 序号 -->
        <el-table-column v-if="showNo" fixed="left" align="center" label="序号" type="index" :index="getNo">
          <template v-slot:header>
            <slot name="col-no-header">#</slot>
          </template>
          <template slot-scope="{row,$index}">
            <div class="nm-list-no">
              <slot name="col-no" :row="row">{{getNo($index)}}</slot>
            </div>
          </template>
        </el-table-column>

        <!-- 自动生成列 -->
        <template v-for="col in columns">
          <el-table-column
            v-if="col.show"
            :key="col.name"
            :prop="col.name"
            :width="col.width"
            :sortable="col.sortable"
            :type="col.type"
            :fixed="col.fixed"
            :align="col.align"
            :header-align="col.headerAlign"
            :size="fontSize"
          >
            <!--自定义头-->
            <template v-slot:header>
              <slot :name="`col-${col.name}-header`">
                <nm-icon v-if="col.icon" :name="col.icon"/>
                {{col.label}}
              </slot>
            </template>

            <template slot-scope="{row}">
              <slot :name="'col-'+col.name" :row="row">{{row[col.name]}}</slot>
            </template>
          </el-table-column>
        </template>

        <!-- 操作列 -->
        <el-table-column v-if="!noOperation" :width="operationWidth" fixed="right" align="center" label="操作">
          <template v-slot:header>
            <slot name="col-operation-header">操作</slot>
          </template>
          <template slot-scope="{row}">
            <div class="nm-list-operation">
              <slot name="col-operation" :row="row"/>
            </div>
          </template>
        </el-table-column>
      </query-table>
    </section>

    <!--footer-->
    <query-footer v-model="page" :total="total" :columns.sync="columns" :no-select-column="noSelectColumn" :reverse="footerReverse">
      <slot name="footer" :total="total" :selection="selection"/>
    </query-footer>
    <slot/>
  </section>
</template>
<script>
import { mapState } from 'vuex'
import { oneOf } from 'nm-lib-utils/src/utils/assist'
import QueryHeader from './components/header'
import QueryToolbar from './components/toolbar'
import QueryTable from './components/table'
import QueryFooter from './components/footer'

// 默认列信息
const defaultColumnInfo = {
  // 列的字段名称
  name: '',
  // 列的显示名称
  label: '',
  // 宽度
  width: '',
  // 排序
  sortable: false,
  // 固定列
  fixed: false,
  // 对其方式
  align: 'center',
  // 表头对其方式
  headerAlign: 'center',
  // 是否显示
  show: true
}
export default {
  name: 'List',
  components: { QueryHeader, QueryToolbar, QueryTable, QueryFooter },
  data () {
    return {
      loading: false,
      fullscreen: false,
      filter: {
        prop: '',
        keyword: ''
      },
      // 分页数据
      page: {
        index: 1,
        size: 15,
        sort: []
      },
      // 数据列表
      rows: [],
      // 总数量
      total: 0,
      selection: [],
      columns: this.cols.map(col => {
        return Object.assign({}, defaultColumnInfo, col)
      })
    }
  },
  props: {
    /** 标题 */
    title: String,
    /** 图标 */
    icon: String,
    /** 搜索设置 */
    search: {
      type: Object
    },
    /** 高级查询中的表单文本宽度 */
    labelWidth: {
      type: String,
      default: '80px'
    },
    // 查询方法
    action: {
      type: Function,
      required: true
    },
    // 条件
    conditions: Object,
    // 条件验证
    conditionsRules: Object,
    // 列数组
    cols: Array,
    /** 多选 */
    multiple: Boolean,
    /** 显示序号 */
    showNo: Boolean,
    /** 不显示操作列 */
    noOperation: Boolean,
    /** 操作列宽度 */
    operationWidth: {
      type: String
    },
    /** 不显示选择列按钮 */
    noSelectColumn: Boolean,
    /** 不显示工具栏 */
    noToolbar: Boolean,
    /** 工具栏对齐方式 */
    toolbarAlign: {
      type: String,
      default: 'left',
      validator (value) {
        return oneOf(value, ['right', 'left'])
      }
    },
    /** 不显示全屏按钮 */
    noFullscreen: Boolean,
    /** 不显示刷新按钮 */
    noRefresh: Boolean,
    /** 不显示头部 */
    noHeader: Boolean,
    /** 底部反转 */
    footerReverse: Boolean,
    /** 合并行列的方法 */
    spanMethod: Function
  },
  computed: {
    ...mapState('app/loading', { loadingText: 'text', loadingBackground: 'background', loadingSpinner: 'spinner' }),
    class_ () {
      return ['nm-list',
        this.fontSize ? `nm-list-${this.fontSize}` : '',
        this.fullscreen ? 'fullscreen' : '']
    }
  },
  methods: {
    /** 查询方法 */
    query () {
      if (this.loading) { return }

      this.loading = true
      let fullConditions = Object.assign({}, this.conditions)

      // 设置分页
      fullConditions.page = this.page

      // 设置关键字
      if (this.filter.keyword) {
        fullConditions[this.filter.prop] = this.filter.keyword
      }
      this.action(fullConditions).then(data => {
        this.rows = data.rows
        this.total = data.total
        this.$refs.table.scrollTop()
        this.loading = false
      }).catch(() => {
        this.loading = false
      })
    },
    /** 刷新 */
    refresh () {
      this.page.index = 1
      this.query()
    },
    /** 查询表单重置 */
    reset () {
      this.$refs.queryForm.reset()
      this.$refs.table.clearSort()
      this.page.index = 1
      this.query()
    },
    /** 获取序号 */
    getNo (index) {
      return (this.page.index - 1) * this.page.size + index + 1
    }
  },
  created () {
    this.query()
  }
}
</script>
