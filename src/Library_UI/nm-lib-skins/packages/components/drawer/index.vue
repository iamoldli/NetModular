<template>
  <section :class="['nm-drawer',placement]">
    <transition name="fade">
      <section class="nm-drawer-modal" @click="handleModalClick" v-if="modal" v-show="visible"></section>
    </transition>
    <transition :name="`move-${placement}`">
      <section
        class="nm-drawer-dialog"
        :style="{width:width}"
        v-show="visible"
        v-loading="loading"
        :element-loading-text="loadingText"
        :element-loading-background="loadingBackground"
        :element-loading-spinner="loadingSpinner"
        v-nm-resizable="optionResizable_"
      >
        <!--头部-->
        <header v-if="header" class="nm-drawer-header">
          <slot name="header">
            <div v-if="icon" class="nm-drawer-header-icon">
              <nm-icon v-if="icon" :name="icon"/>
            </div>
            <!--标题-->
            <div class="nm-drawer-header-title">{{title}}</div>
            <!--工具栏-->
            <div ref="toolbar" class="nm-drawer-header-toolbar">
              <!--工具栏插槽-->
              <slot name="toolbar"/>
              <!--关闭按钮-->
              <nm-button icon="close" @click="close"/>
            </div>
          </slot>
        </header>
        <section class="nm-drawer-body">
          <!-- <div class="nm-resizable-move-trigger" v-nm-resizable="optionResizable">
            <div class="nm-resizable-move-point">
              <i/>
              <i/>
              <i/>
              <i/>
            </div>
          </div>-->
          <section class="nm-drawer-body-wrapper">
            <nm-scrollbar ref="scrollbar" :horizontal="horizontal">
              <slot/>
            </nm-scrollbar>
          </section>
        </section>
        <footer v-if="footer" class="nm-drawer-footer">
          <slot name="footer"></slot>
        </footer>
      </section>
    </transition>
  </section>
</template>
<script>
import { mapState } from 'vuex'
import { oneOf } from 'nm-lib-utils/src/utils/assist'
export default {
  name: 'Drawer',
  data () {
    return {

    }
  },
  props: {
    /** 是否显示 */
    visible: Boolean,
    /** 标题 */
    title: String,
    /** 图标 */
    icon: String,
    /** 位置 */
    placement: {
      type: String,
      default: 'right',
      validator (value) {
        return oneOf(value, ['left', 'right'])
      }
    },
    /** 宽度 */
    width: {
      type: String,
      default: '30%'
    },
    /** 是否显示头部 */
    header: Boolean,
    /** 是否显示底部 */
    footer: Boolean,
    /** 是否显示水平滚动条 */
    horizontal: Boolean,
    /** 工具栏 */
    toolbar: Array,
    /** loading */
    loading: Boolean,
    /** 是否附加到Body，默认附加到nm-container */
    appendToBody: Boolean,
    /** 是否显示模态框 */
    modal: {
      type: Boolean,
      default: true
    },
    /** 是否点击模态框关闭抽屉 */
    modalClickClose: {
      type: Boolean,
      default: true
    },
    /** 是否可以拖动 */
    isResizable: {
      type: Boolean,
      default: true
    }
  },
  computed: {
    ...mapState('app/loading', { loadingText: 'text', loadingBackground: 'background', loadingSpinner: 'spinner' }),
    optionResizable_ () {
      let p = this.placement === 'left' ? ['right'] : ['left']

      return {
        resizableDirection: p,
        enabled: this.isResizable,
        limitNode: '.nm-content',
        customElem: '<div class="nm-resizable-move-trigger nm-resizable-move-trigger-' + this.placement + '"> <div class="nm-resizable-move-point"> <i/><i/><i/><i/></div> </div>'
      }
    }
  },
  methods: {
    append () {
      let parentNode
      if (this.appendToBody) {
        // 附加到body下面
        parentNode = document.body
      } else {
        // 附加到nm-content下面
        parentNode = document.querySelector('.nm-content')
      }
      parentNode.appendChild(this.$el)

      window.addEventListener('resize', this.resize)
    },
    close () {
      this.$emit('update:visible', false)
      this.$emit('close')
    },
    resize () {
      this.$refs.scrollbar.update()
    },
    handleModalClick () {
      if (this.modal && this.modalClickClose) {
        this.close()
      }
    }
  },
  mounted () {
    this.append()
  },
  destroyed () {
    if (this.$el && this.$el.parentNode) {
      this.$el.parentNode.removeChild(this.$el)
      window.removeEventListener('resize', this.resize)
    }
  }
}
</script>
