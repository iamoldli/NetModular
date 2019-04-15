<template>
  <el-dialog
    ref="dialog"
    v-nm-drop="draggableOption_"
    v-nm-resizable="resizableOption_"
    :id="id"
    :class="class_"
    :top="top"
    :modal="modal_"
    :close-on-click-modal="closeOnClickModal_"
    :fullscreen="fullscreen_"
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
      <section class="nm-dialog-title">
        <slot name="title">{{title}}</slot>
      </section>
      <section class="nm-dialog-toolbar">
        <!--工具栏插槽-->
        <slot name="toolbar"/>
        <!--全屏按钮-->
        <nm-button v-if="fullscreen" :icon="fullscreen_ ? 'fullscreen-c':'fullscreen-o'" @click="toggerFullscreen"/>
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
      <section v-if="!noFooter" class="nm-dialog-footer">
        <slot name="footer"/>
      </section>
    </section>
  </el-dialog>
</template>
<script>
import { mapState, mapActions } from 'vuex'
import { addResizeListener, removeResizeListener } from 'element-ui/src/utils/resize-event'
import dialog from '../../mixins/components/dialog.js'
export default {
  name: 'Dialog',
  mixins: [dialog],
  data () {
    return {
      id: '',
      // 全屏
      fullscreen_: false,
      // 距顶部高度
      top: '',
      dropOldStyle: null,
      on: {
        open: this.onOpen,
        opened: this.onOpened,
        close: this.onClose,
        closed: this.onClosed
      }
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
    /** 是否需要遮罩层 */
    modal: {
      type: Boolean,
      default: true
    },
    /** 是否可以通过点击 modal 关闭 Dialog */
    closeOnClickModal: {
      type: Boolean,
      default: true
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
    /** 不显示尾部 */
    noFooter: Boolean,
    /** 是否可拖拽 */
    draggable: {
      type: Boolean,
      default: false
    },
    /** 拖拽容器 */
    dragContainer: {
      type: String,
      default: 'body'
    },
    /** 显示加载动画 */
    loading: Boolean
  },
  computed: {
    ...mapState('app/loading', { loadingText: 'text', loadingBackground: 'background', loadingSpinner: 'spinner' }),
    elScrollbarViewEl () {
      return this.$refs.dialog.$el.querySelector('.el-scrollbar__view')
    },
    class_ () {
      return ['nm-dialog', this.draggableOption_.enabled ? 'draggable' : '']
    },
    /** 可以拖动时，点击空白处不可关闭**/
    closeOnClickModal_ () {
      return !this.draggable && this.closeOnClickModal
    },
    modal_ () {
      return !this.draggable && this.modal
    },
    width_ () {
      return typeof this.width === 'number' ? this.width > 0 ? this.width + 'px' : '50%' : this.width
    },
    draggableOption_ () {
      return {
        // 是否启用
        enabled: this.fullscreen_ ? false : this.draggable,
        // 拖动节点
        dropNode: '.el-dialog__header>.nm-dialog-title',
        // 限制节点
        limitNode: this.dragContainer
      }
    },
    resizableOption_ () {
      return {
        // 是否启用
        enabled: this.fullscreen_ ? false : this.draggable,
        // 调整方向
        resizableDirection: ['left-top', 'left', 'top', 'right', 'right-top', 'bottom', 'left-bottom', 'right-bottom'],
        // 插入节点筛选器
        parentNode: '.el-dialog',
        // 限制节点
        limitNode: this.dragContainer,
        // 样式
        class: ''
      }
    }
  },
  methods: {
    ...mapActions('app/dialog', ['open']),
    /** 全屏切换 */
    toggerFullscreen () {
      this.fullscreen_ = !this.fullscreen_
    },
    /** 开启全屏 */
    openFullscreen () {
      this.fullscreen_ = true
    },
    /** 关闭全屏 */
    closeFullscreen () {
      this.fullscreen_ = false
    },
    /** 关闭对话框 */
    close () {
      this.hide()
    },
    /** 保存样式 */
    saveStyle () {
      // 缓存参数
      let elem = this.$refs.dialog.$el
      if (elem.style.width === '') { return }
      this.dropOldStyle = {}
      this.dropOldStyle.width = elem.style.width
      this.dropOldStyle.height = elem.style.height
      this.dropOldStyle.top = elem.style.top
      this.dropOldStyle.left = elem.style.left
    },
    /**
    * 调整高度
    */
    resize () {
      this.saveStyle()
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

      // 拖拽模式
      if (this.draggable && !this.fullscreen_) {
        const wrapperEl = this.$refs.dialog.$el
        if (this.dropOldStyle === null) {
          wrapperEl.style.width = this.width_
          wrapperEl.style.top = this.top
          wrapperEl.style.left = (document.body.clientWidth - wrapperEl.clientWidth) / 2 + 'px'
          wrapperEl.style.height = height
        } else {
          // 还原样式
          wrapperEl.style.width = this.dropOldStyle.width
          wrapperEl.style.height = this.dropOldStyle.height
          wrapperEl.style.top = this.dropOldStyle.top
          wrapperEl.style.left = this.dropOldStyle.left
        }
      } else {
        this.$refs.dialog.$el.querySelector('.el-dialog').style.height = height
        this.$refs.dialog.$el.querySelector('.el-dialog').style.width = this.width_
      }
      if (!this.noScrollbar) {
        this.$nextTick(() => {
          this.$refs.scrollbar.update()
        })
      }
    },
    // 获取对话框的高度信息
    getDialogHeight () {
      const el = this.$refs.dialog.$el
      const header = el.querySelector('.el-dialog__header')
      const h = header ? header.offsetHeight : 0
      let b = this.elScrollbarViewEl ? this.elScrollbarViewEl.offsetHeight : 0
      const footer = el.querySelector('.nm-dialog-footer')
      const f = footer ? footer.offsetHeight : 0

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
    onOpen () {
      this.$nextTick(() => {
        window.addEventListener('resize', this.resize)
        this.resize()
        if (!this.noScrollbar) { addResizeListener(this.elScrollbarViewEl, this.resize) }
      })
      this.$emit('open')
    },
    onOpened () {
      this.$emit('opened')
    },
    onClose () {
      this.saveStyle()
      window.removeEventListener('resize', this.resize)
      if (!this.noScrollbar) { removeResizeListener(this.elScrollbarViewEl, this.resize) }
      this.$emit('close')
    },
    onClosed () {
      this.$emit('closed')
    }
  },
  mounted () {
    // 打开对话框
    this.open().then(id => {
      this.id = 'nm-dialog-' + id
    })
  },
  watch: {
    draggableOption_ () {
      if (!this.draggableOption_.enabled && this.visible) {
        // 缓存参数
        this.saveStyle()
        let elem = this.$refs.dialog.$el
        elem.style.height = null
        elem.style.top = null
        elem.style.width = null
        elem.style.left = null
      } else {
        this.resize()
      }
    }
  }
}
</script>
