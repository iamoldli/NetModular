<template>
  <section
    :class="class_"
    :style="style_"
    v-loading="loading"
    :element-loading-text="loadingText"
    :element-loading-background="loadingBackground"
    :element-loading-spinner="loadingSpinner"
  >
    <!--头部-->
    <header v-if="header" class="nm-box-header">
      <slot name="header">
        <div v-if="icon" class="nm-box-header-icon">
          <nm-icon v-if="icon" :name="icon" />
        </div>
        <!--标题-->
        <div class="nm-box-header-title">
          <slot name="title">{{title}}</slot>
        </div>
        <!--工具栏之前-->
        <div class="nm-box-header-toobar-before">
          <slot name="toolbar-before" />
        </div>
        <!--工具栏-->
        <div ref="toolbar" class="nm-box-header-toolbar">
          <!--工具栏插槽-->
          <slot name="toolbar" />
          <!--全屏按钮-->
          <nm-button v-if="fullscreen" :icon="this.fullscreen_ ? 'min' : 'max'" @click="onFullscreen" />
          <!--刷新按钮-->
          <nm-button v-if="refresh" icon="refresh" @click="refreshAction" />
          <!--折叠按钮-->
          <nm-button v-if="collapse" :icon="this.collapse_ ? 'angle-down':'angle-up'" @click="onCollapse" />
        </div>
      </slot>
    </header>
    <el-collapse-transition>
      <section class="nm-box-dialog" v-show="!collapse_">
        <section class="nm-box-content">
          <section class="nm-box-wrapper" v-if="hasScrollbar">
            <nm-scrollbar ref="scrollbar" :horizontal="horizontal">
              <slot />
            </nm-scrollbar>
          </section>
          <slot v-else />
        </section>
        <footer v-if="footer" class="nm-box-footer">
          <slot name="footer"></slot>
        </footer>
      </section>
    </el-collapse-transition>
  </section>
</template>
<script>
import { mapState } from 'vuex'
export default {
  name: 'Box',
  data() {
    return {
      collapse_: false,
      fullscreen_: false
    }
  },
  props: {
    /** 标题 */
    title: String,
    /** 图标 */
    icon: String,
    /** 是否显示头部 */
    header: Boolean,
    /** 是否显示底部 */
    footer: Boolean,
    /** 高度 */
    height: String,
    /** 是否显示顶部边框 */
    border: Boolean,
    /** 顶部边框的颜色 */
    borderColor: {
      type: String,
      default: 'success'
    },
    /** 标题是否加粗 */
    titleBold: Boolean,
    /** 是否显示水平滚动条 */
    horizontal: Boolean,
    /** 自定义折叠事件 */
    customCollapseEvent: Function,
    /** loading */
    loading: Boolean,
    /** 是否页模式 */
    page: Boolean,
    /** 是否显示全屏按钮 */
    fullscreen: Boolean,
    /** 是否显示折叠按钮 */
    collapse: Boolean,
    /** 是否显示滚动条 */
    noScrollbar: Boolean,
    /** 没有内边距 */
    noPadding: Boolean,
    /** 是否显示刷新按钮 */
    refresh: Boolean,
    /** 刷新按钮点击事件 */
    refreshAction: Function
  },
  computed: {
    ...mapState('app/loading', { loadingText: 'text', loadingBackground: 'background', loadingSpinner: 'spinner' }),
    /** 生成class */
    class_() {
      let classArr = [
        'nm-box',
        this.fontSize,
        this.fullscreen_ ? 'fullscreen' : '',
        this.height ? 'has-height' : '',
        this.page ? 'page' : '',
        this.noPadding ? 'no-padding' : '',
        this.border ? 'border' : '',
        this.titleBold ? 'title-bold' : '']

      if (['success', 'primary', 'info', 'warning', 'error'].includes(this.borderColor)) {
        classArr.push(this.borderColor)
      }
      return classArr
    },
    style_() {
      let style = { height: this.height }
      if (!['success', 'primary', 'info', 'warning', 'error'].includes(this.borderColor)) {
        style.borderTopCoder = this.borderColor
      }
      return style
    },
    /** 是否有滚动条 */
    hasScrollbar() {
      return !this.noScrollbar && (this.height || this.page)
    },
    /** 是否可以折叠 */
    isCollapse() {
      return this.collapse && !this.page
    }
  },
  methods: {
    /** 开启全屏 */
    openFullscreen() {
      this.fullscreen_ = true
      // 全屏事件
      this.$emit('fullscreen-change', this.fullscreen_)
    },
    /** 关闭全屏 */
    closeFullscreen() {
      this.fullscreen_ = false
      // 全屏事件
      this.$emit('fullscreen-change', this.fullscreen_)
    },
    /** 滚动条重置 */
    scrollbarResize() {
      if (this.hasScrollbar) {
        this.$refs.scrollbar.update()
      }
    },
    /** 折叠事件 */
    onCollapse() {
      // 如果设置了自定义折叠事件，则覆盖默认的
      if (this.customCollapseEvent) {
        this.collapse_ = this.customCollapseEvent()
      } else if (this.isCollapse) {
        this.collapse_ = !this.collapse_
      }
      // 折叠事件
      this.$emit('collapse-change', this.collapse_)
    },
    /** 全屏事件 */
    onFullscreen() {
      if (this.fullscreen) {
        this.fullscreen_ = !this.fullscreen_

        // 全屏事件
        this.$emit('fullscreen-change', this.fullscreen_)
      }
    }
  }
}
</script>
