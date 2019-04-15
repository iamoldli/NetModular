<template>
  <section
    class="nm-box"
    :class="class_"
    :style="{height}"
    v-loading="loading"
    :element-loading-text="loadingText"
    :element-loading-background="loadingBackground"
    :element-loading-spinner="loadingSpinner"
  >
    <!--头部-->
    <header v-if="header" class="nm-box-header">
      <slot name="header">
        <div v-if="icon" class="nm-box-header-icon">
          <nm-icon v-if="icon" :name="icon"/>
        </div>
        <!--标题-->
        <div class="nm-box-header-title">{{title}}</div>
        <!--工具栏之前-->
        <div class="nm-box-header-toobar-before">
          <slot name="toolbar-before"/>
        </div>
        <!--工具栏-->
        <div ref="toolbar" class="nm-box-header-toolbar">
          <!--工具栏插槽-->
          <slot name="toolbar"/>
          <!--全屏按钮-->
          <nm-button v-if="fullscreen" :icon="this.fullscreen_ ? 'min' : 'max'" @click="onFullscreen"/>
          <!--折叠按钮-->
          <nm-button v-if="collapse" :icon="this.collapse_ ? 'angle-down':'angle-up'" @click="onCollapse"/>
        </div>
      </slot>
    </header>
    <el-collapse-transition>
      <section class="nm-box-dialog" v-show="!collapse_">
        <section class="nm-box-content">
          <section class="nm-box-wrapper" v-if="hasScrollbar">
            <nm-scrollbar ref="scrollbar" :horizontal="horizontal">
              <slot/>
            </nm-scrollbar>
          </section>
          <slot v-else/>
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
  data () {
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
    collapse: Boolean
  },
  computed: {
    ...mapState('app/loading', { loadingText: 'text', loadingBackground: 'background', loadingSpinner: 'spinner' }),
    /** 生成class */
    class_ () {
      return [
        this.fontSize,
        this.fullscreen_ ? 'fullscreen' : '',
        this.height ? 'has-height' : '',
        this.page ? 'page' : '']
    },
    /** 是否有滚动条 */
    hasScrollbar () {
      return this.height || this.page
    }
  },
  methods: {
    /** 开启全屏 */
    openFullscreen () {
      this.fullscreen_ = true
    },
    /** 关闭全屏 */
    closeFullscreen () {
      this.fullscreen_ = false
    },
    onCollapse () {
      // 如果设置了自定义折叠事件，则覆盖默认的
      if (this.customCollapseEvent) {
        this.collapse_ = this.customCollapseEvent()
      } else if (this.isCollapse) {
        this.collapse_ = !this.collapse_
      }
    },
    onFullscreen () {
      if (this.fullscreen) this.fullscreen_ = !this.fullscreen_
    },
    scrollbarResize () {
      if (this.hasScrollbar) {
        this.$refs.scrollbar.update()
      }
    }
  }
}
</script>
