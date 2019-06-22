<template>
  <el-dialog
    ref="dialog"
    :id="id"
    :class="class_"
    :top="draggable?'':top"
    :modal="modal_"
    :close-on-click-modal="closeOnClickModal_"
    :fullscreen="hasFullscreen"
    :visible.sync="visible_"
    :show-close="false"
    :append-to-body="true"
    v-on="on"
  >
    <!--头部-->
    <template v-slot:title>
      <section v-if="icon" class="nm-dialog-icon">
        <nm-icon :name="icon"/>
      </section>
      <section ref="title" class="nm-dialog-title">
        <slot name="title">{{title}}</slot>
      </section>
      <section class="nm-dialog-toolbar">
        <!--工具栏插槽-->
        <slot name="toolbar"/>
        <!--全屏按钮-->
        <nm-button v-if="fullscreen" :icon="hasFullscreen ? 'fullscreen-c':'fullscreen-o'" @click="toggerFullscreen"/>
        <!--关闭按钮-->
        <nm-button icon="close" @click="close"/>
      </section>
    </template>

    <!--Body-->
    <section
      ref="dialogBody"
      class="nm-dialog-body"
      v-loading="loading"
      :element-loading-text="loadingText"
      :element-loading-background="loadingBackground"
      :element-loading-spinner="loadingSpinner"
    >
      <section class="nm-dialog-content">
        <section class="nm-dialog-main">
          <slot v-if="noScrollbar"/>
          <nm-scrollbar v-else ref="scrollbar" class="nm-dialog-body-scrollbar">
            <slot/>
          </nm-scrollbar>
        </section>
      </section>
      <section v-if="footer" class="nm-dialog-footer">
        <slot name="footer"/>
      </section>
    </section>
  </el-dialog>
</template>
<script>
import { mapState, mapActions } from 'vuex'
import { addResizeListener, removeResizeListener } from 'element-ui/src/utils/resize-event'
import { on, off } from 'nm-lib-utils/src/utils/dom'
import dialog from '../../mixins/components/dialog.js'
export default {
  name: 'Dialog',
  mixins: [dialog],
  data() {
    return {
      id: '',
      // 是否已全屏
      hasFullscreen: false,
      // 距顶部高度
      top: '',
      // 是否点击拖拽
      isDragDown: false,
      on: {
        open: this.onOpen,
        opened: this.onOpened,
        close: this.onClose,
        closed: this.onClosed
      },
      // 拖拽点击时的状态
      dragDownState: {},
      // 已初始化
      hasInit: false
    }
  },
  props: {
    /** Dialog 的标题，也可通过具名 title传入 */
    title: String,
    /** 图标 */
    icon: String,
    /** Dialog 的宽度 */
    width: {
      type: [Number, String],
      default: '50%'
    },
    /** Dialog 的高度 */
    height: [Number, String],
    /** 显示尾部 */
    footer: Boolean,
    /** 是否需要遮罩层 */
    modal: {
      type: Boolean,
      default: true
    },
    /** 是否可以通过点击 modal 关闭 Dialog */
    closeOnClickModal: {
      type: Boolean,
      default: null
    },
    /** 是否显示关闭按钮 */
    showClose: {
      type: Boolean,
      default: true
    },
    /** 关闭前的回调，会暂停 Dialog 的关闭 */
    beforeClose: Function,
    /** 是否显示全屏按钮 */
    fullscreen: Boolean,
    /** 不包含滚动条 */
    noScrollbar: {
      type: Boolean,
      default: false
    },
    /** 显示加载动画 */
    loading: Boolean,
    /** 可拖拽的 */
    draggable: Boolean,
    /** 是否可拖出页面 */
    dragOutPage: Boolean,
    /** 拖拽出页面后保留的最小宽度 */
    dragMinWidth: {
      type: Number,
      default: 100
    }
  },
  computed: {
    ...mapState('app/system', ['dialogCloseOnClickModal']),
    ...mapState('app/loading', { loadingText: 'text', loadingBackground: 'background', loadingSpinner: 'spinner' }),
    elScrollbarViewEl() {
      return this.$refs.dialog.$el.querySelector('.el-scrollbar__view')
    },
    class_() {
      return ['nm-dialog', this.draggable ? 'draggable' : '']
    },
    width_() {
      return typeof this.width === 'number' ? this.width > 0 ? this.width + 'px' : '50%' : this.width
    },
    modal_() {
      return !this.draggable && this.modal
    },
    closeOnClickModal_() {
      if (this.closeOnClickModal === null) {
        return this.dialogCloseOnClickModal
      }
      return this.closeOnClickModal
    },
    dialogEl() {
      return this.$refs.dialog.$el.querySelector('.el-dialog')
    },
    titleEl() {
      return this.$refs.title
    }
  },
  methods: {
    ...mapActions('app/dialog', ['open']),
    /** 全屏切换 */
    toggerFullscreen() {
      this.hasFullscreen = !this.hasFullscreen
    },
    /** 开启全屏 */
    openFullscreen() {
      this.hasFullscreen = true
    },
    /** 关闭全屏 */
    closeFullscreen() {
      this.hasFullscreen = false
    },
    /** 关闭对话框 */
    close() {
      this.hide()
    },
    /**
    * 调整高度
    */
    resize() {
      // 对话框高度
      const dialogHeight = this.getDialogHeight()
      // 页面高度
      const bodyHeight = document.body.clientHeight

      let height
      if (dialogHeight.full + 20 > bodyHeight) {
        this.top = '10px'
        height = bodyHeight - dialogHeight.h - dialogHeight.f - 20 + 'px'
      } else {
        this.top = (bodyHeight - dialogHeight.full) / 7 * 2.5 + 'px'
        height = dialogHeight.full + 'px'
      }

      this.dialogEl.style.height = height
      this.dialogEl.style.width = this.width_

      this.updateScrollbar()
    },
    // 获取对话框的高度信息
    getDialogHeight() {
      const headerEl = this.dialogEl.querySelector('.el-dialog__header')
      const h = headerEl ? headerEl.offsetHeight : 0
      let b = this.elScrollbarViewEl ? this.elScrollbarViewEl.offsetHeight : 0
      const footerEl = this.dialogEl.querySelector('.nm-dialog-footer')
      const f = footerEl ? footerEl.offsetHeight : 0

      let full = h + b + f
      // 如果主动设置了高度
      if (this.height) {
        if (typeof this.height === 'number' && this.height > 0) {
          full = this.height
        } else {
          if (this.height.endsWith('px')) {
            full = parseFloat(this.height.replace('px', ''))
          } else {
            full = document.body.clientHeight * parseFloat(this.height.replace('%', '')) / 100 - h - f
          }
        }
        b = parseFloat(full - h - f)
      }

      return { h, b, f, full: full }
    },
    /**
     * @description 更新滚动条
     */
    updateScrollbar() {
      // 如果有滚动条，需要更新下滚动条
      if (!this.noScrollbar) {
        this.$nextTick(() => {
          this.$refs.scrollbar.update()
        })
      }
    },
    /**
     * @description 绑定拖拽功能
     */
    bindDrag() {
      if (!this.draggable) return
      on(this.titleEl, 'mousedown', this.handleDragDown)
    },
    /**
     * @description 处理拖拽点击
     */
    handleDragDown(e) {
      if (this.draggable && !this.hasFullscreen) {
        this.isDragDown = true
        this.dragDownState = e

        // 防止鼠标选中抽屉中文字，造成拖动trigger触发浏览器原生拖动行为
        window.getSelection ? window.getSelection().removeAllRanges() : document.selection.empty()
      }
    },
    /**
     * @description 处理拖拽移动
     */
    handleDragMove(e) {
      if (this.isDragDown) {
        let left = this.dialogEl.offsetLeft + (e.clientX - this.dragDownState.clientX)
        let top = this.dialogEl.offsetTop + (e.clientY - this.dragDownState.clientY)
        let leftMax = document.body.offsetWidth - this.dialogEl.offsetWidth
        let leftMin = 0
        let topMax = document.body.offsetHeight - this.dialogEl.offsetHeight
        let topMin = 0

        if (this.dragOutPage) {
          leftMax = document.body.offsetWidth - this.dragMinWidth
          leftMin = -this.dialogEl.offsetWidth + this.dragMinWidth
          topMax = document.body.offsetHeight - this.titleEl.offsetHeight
        }

        this.dialogEl.style.left = Math.max(leftMin, Math.min(left, leftMax)) + 'px'
        this.dialogEl.style.top = Math.max(topMin, Math.min(top, topMax)) + 'px'
        this.dragDownState = e
      }
    },
    /**
     * @description 处理拖拽结束
     */
    handleDragUp(e) {
      this.isDragDown = false
    },
    onOpen() {
      this.$nextTick(() => {
        this.resize()
        this.bindDrag()

        on(window, 'resize', this.resize)
        if (!this.noScrollbar) { addResizeListener(this.elScrollbarViewEl, this.updateScrollbar) }
      })

      this.$emit('open')
    },
    onOpened() {
      if (!this.hasInit) {
        // 如果是可拖拽的，需要计算绝对定位
        if (this.draggable) {
          this.dialogEl.style.left = (document.body.offsetWidth - this.dialogEl.offsetWidth) / 2 + 'px'
          this.dialogEl.style.top = this.top
        }

        this.hasInit = true
      }
      this.$emit('opened')
    },
    onClose() {
      off(window, 'resize', this.resize)
      if (!this.noScrollbar) { removeResizeListener(this.elScrollbarViewEl, this.updateScrollbar) }
      this.$emit('close')
    },
    onClosed() {
      this.$emit('closed')
    }
  },
  mounted() {
    // 打开对话框
    this.open().then(id => {
      this.id = 'nm-dialog-' + id
    })
    console.log(this.draggable)
    on(document, 'mousemove', this.handleDragMove)
    on(document, 'mouseup', this.handleDragUp)
  },
  destroyed() {
    off(document, 'mousemove', this.handleDragMove)
    off(document, 'mouseup', this.handleDragUp)
  }
}
</script>
