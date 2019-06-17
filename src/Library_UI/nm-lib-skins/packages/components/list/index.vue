<template>
  <section
    :class="class_"
    v-loading="showLoading"
    :element-loading-text="loadingText||loadingText_"
    :element-loading-background="loadingBackground"
    :element-loading-spinner="loadingSpinner"
  >
    <!--header-->
    <query-header v-if="!noHeader" :title="title" :icon="icon" :no-fullscreen="noFullscreen" :fullscreen.sync="fullscreen" :no-refresh="noRefresh">
      <template v-slot:toolbar>
        <slot name="header-toolbar" :total="total" :selection="selection"/>
      </template>
    </query-header>

    <!--查询栏-->
    <querybar ref="querybar" v-if="!noQuerybar" v-bind="querybar">
      <template v-slot>
        <slot name="querybar"/>
      </template>
      <template v-slot:buttons>
        <slot name="querybar-buttons" :total="total" :selection="selection"/>
      </template>
      <template v-slot:advanced>
        <slot name="querybar-advanced"/>
      </template>
    </querybar>

    <section class="nm-list-body">
      <query-table ref="table" :rows="rows" :cols="cols" :span-method="spanMethod" :selection.sync="selection">
        <!-- 多选 -->
        <el-table-column v-if="multiple" fixed="left" align="center" type="selection" width="55"/>

        <!-- 序号 -->
        <el-table-column v-if="showNo" fixed="left" align="center" type="index" :index="getNo">
          <template v-slot:header>
            <slot name="col-no-header">序号</slot>
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
    <query-footer v-if="!noFooter" v-model="page" :total="total" :columns.sync="columns" :no-select-column="noSelectColumn" :reverse="footerReverse">
      <slot name="footer" :total="total" :selection="selection"/>
    </query-footer>
    <slot/>
  </section>
</template>
<script>
import { mapState } from 'vuex'
import QueryHeader from './components/header'
import Querybar from './components/querybar'
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
  components: { QueryHeader, Querybar, QueryTable, QueryFooter },
  data() {
    return {
      loading_: false,
      fullscreen: false,
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
    // 查询方法
    action: {
      type: Function,
      required: true
    },
    /** 查询表单输入框宽度 */
    inputWidth: String,
    // 模型
    model: Object,
    /** 模型验证规则 */
    rules: Object,
    /** 高级查询 */
    advanced: Object,
    // 列数组
    cols: Array,
    /** 多选 */
    multiple: Boolean,
    /** 显示序号 */
    showNo: {
      type: Boolean,
      default: true
    },
    /** 不显示操作列 */
    noOperation: Boolean,
    /** 操作列宽度 */
    operationWidth: [String, Number],
    /** 不显示选择列按钮 */
    noSelectColumn: Boolean,
    /** 不显示查询栏 */
    noQuerybar: Boolean,
    /** 不显示全屏按钮 */
    noFullscreen: Boolean,
    /** 不显示刷新按钮 */
    noRefresh: Boolean,
    /** 不显示头部 */
    noHeader: Boolean,
    /** 不显示底部 */
    noFooter: Boolean,
    /** 不包含搜索功能 */
    noSearch: Boolean,
    /** 不显示查询按钮图标 */
    noSearchButtonIcon: Boolean,
    /** 底部反转 */
    footerReverse: Boolean,
    /** 合并行列的方法 */
    spanMethod: Function,
    /** 加载中动画 */
    loading: Boolean,
    /** 加载中文本 */
    loadingText: String,
    /** 创建后执行一次查询 */
    queryOnCreated: {
      type: Boolean,
      default: true
    }
  },
  computed: {
    ...mapState('app/loading', { loadingText_: 'text', loadingBackground: 'background', loadingSpinner: 'spinner' }),
    class_() {
      return ['nm-list',
        this.fontSize ? `nm-list-${this.fontSize}` : '',
        this.fullscreen ? 'fullscreen' : '']
    },
    querybar() {
      return {
        model: this.model,
        rules: this.rules,
        inputWidth: this.inputWidth,
        advanced: this.advanced,
        noSearch: this.noSearch,
        noSearchButtonIcon: this.noSearchButtonIcon
      }
    },
    showLoading() {
      return this.loading || this.loading_
    }
  },
  methods: {
    /** 查询方法 */
    query() {
      if (this.loading_) { return }

      this.loading_ = true
      let fullModel = Object.assign({}, this.model)

      // 设置分页
      fullModel.page = this.page

      this.action(fullModel).then(data => {
        this.rows = data.rows
        this.total = data.total
        this.$refs.table.scrollTop()
        this.loading_ = false
      }).catch(() => {
        this.loading_ = false
      })
    },
    /** 刷新 */
    refresh() {
      this.page.index = 1
      this.query()
    },
    /** 查询表单重置 */
    reset(from) {
      if (!from) {
        this.$refs.querybar.reset()
      } else {
        this.$refs.table.clearSort()
        this.page.index = 1
      }
    },
    /** 获取序号 */
    getNo(index) {
      return (this.page.index - 1) * this.page.size + index + 1
    }
  },
  created() {
    if (this.queryOnCreated) {
      this.query()
    }
  }
}
</script>
