<template>
  <section class="nm-list-toolbar">
    <section class="nm-list-toolbar-left" :style="{textAlign:toolbarAlign}">
      <slot name="toolbar"/>
    </section>
    <section class="nm-list-toolbar-right">
      <div class="nm-list-search">
        <el-input
          class="nm-list-search-input"
          :style="{width:search_.width}"
          v-model="keyword"
          :placeholder="searchPlaceholder"
          clearable
          @click.native="onSearchInputClick"
          @clear="onSearchInputClear"
        >
          <template v-slot:suffix>
            <nm-icon v-show="!keyword" name="search"/>
          </template>
        </el-input>

        <el-collapse-transition>
          <div v-show="queryBoxShow" class="nm-list-search-box">
            <div class="nm-list-search-box-body">
              <ul class="nm-list-search-list">
                <li v-for="item in filters" :key="item.prop" @click="onFiltersSelect(item)">
                  <span>{{item.label}}包含【{{keyword}}】</span>
                </li>
              </ul>
            </div>
            <footer v-if="search_.advanced.enabled" class="nm-list-search-box-footer">
              <nm-button ref="advancedButton" type="primary" text="高级查询" @click="onAdvancedQueryClick"/>
            </footer>
          </div>
        </el-collapse-transition>

        <!--高级查询-->
        <transition name="el-zoom-in-top">
          <div ref="advanced" v-show="advancedQueryShow" class="nm-list-search-advanced" :style="advancdeStyle">
            <nm-box page header footer title="高级查询" icon="search" :toolbar="null">
              <template v-slot:toolbar>
                <nm-button icon="close" @click="advancedQueryShow=false"/>
              </template>
              <!--查询条件-->
              <slot name="querybar"></slot>
              <template v-slot:footer>
                <!--查询按钮-->
                <nm-button type="primary" @click="query" text="查询"/>
                <!--重置按钮-->
                <nm-button type="info" @click="reset" text="重置"/>
              </template>
            </nm-box>
            <div class="advanced-arrow"/>
          </div>
        </transition>
      </div>
    </section>
  </section>
</template>
<script>
import { on, off } from 'nm-lib-utils/src/utils/dom'

/** 搜索设置 */
const searchOptions = {
  // 是否开启
  enabled: true,
  // 搜索框宽度
  width: '180px',
  // 高级搜索
  advanced: {
    // 是否开启
    enabled: true,
    // 搜索框提示文本
    placeholder: '',
    // 高级搜索框宽度
    width: '600px',
    // 高级搜索框高度
    height: ''
  }
}
export default {
  data () {
    return {
      // 搜索框关键字
      keyword: '',
      // 是否显示高级查询框
      queryBoxShow: false,
      // 高级查询
      advancedQueryShow: false
    }
  },
  props: {
    /** 列信息 */
    columns: Array,
    /** 搜索设置 */
    search: {
      type: Object,
      default () {
        return searchOptions
      }
    },
    toolbarAlign: String
  },
  computed: {
    filters () {
      return this.keyword ? this.columns.filter(item => item.suggest) : []
    },
    search_ () {
      return this.$_.merge({}, searchOptions, this.search)
    },
    searchPlaceholder () {
      if (this.search_.advanced.placeholder) {
        return this.search_.advanced.placeholder
      }
      const labels = this.columns.filter(item => item.suggest).map(item => item.label)
      return `搜索：${this.$_.join(labels, '、')}`
    },
    advancdeStyle () {
      return { width: this.search_.advanced.width, height: this.search_.advanced.height }
    }
  },
  methods: {
    /** 选择过滤条件 */
    onFiltersSelect (item) {
      const selection = { prop: item.name, keyword: this.keyword }
      this.$emit('input', selection)
      this.queryBoxShow = false

      this.$parent.refresh()
    },
    onSearchInputClick () {
      this.queryBoxShow = !this.queryBoxShow
      this.advancedQueryShow = false
    },
    onSearchInputClear () {
      this.$emit('input', {})
      this.refresh()
    },
    onAdvancedQueryClick () {
      this.queryBoxShow = false
      this.advancedQueryShow = true
    },
    onDocumentClick (e) {
      let advanced = this.$refs.advanced
      let advancedButton = this.$refs.advancedButton ? this.$refs.advancedButton.$el : null
      if (!this.advancedQueryShow || advanced.contains(e.target) || (advancedButton && advancedButton.contains(e.target))) { return }
      if (document.body.contains(e.target)) { this.advancedQueryShow = false }
    },
    query () {
      this.$emit('input', {})
      this.$parent.query()
    },
    reset () {
      this.$parent.reset()
    },
    refresh () {
      this.$parent.refresh()
    },
    // 重置高级查询框
    resizeAdvancedBox () {
      if (!this.search_.advanced.height) {
        this.$nextTick(() => {
          const el = this.$refs.advanced
          const header = el.querySelector('.nm-box-header').offsetHeight
          const footer = el.querySelector('.nm-box-footer').offsetHeight
          const view = el.querySelector('.el-scrollbar__view').offsetHeight
          el.style.height = header + footer + view + 10 + 'px'
        })
      }
    }
  },
  mounted () {
    on(document, 'click', this.onDocumentClick)
  },
  watch: {
    advancedQueryShow (val) {
      if (val) {
        this.resizeAdvancedBox()
      }
    }
  },
  destroyed () {
    off(document, 'click', this.onDocumentClick)
  }
}
</script>
