<template>
  <nm-dialog class="nm-list-dialog" v-bind="dialog" v-on="on" :visible.sync="visible_">
    <template v-slot:toolbar>
      <!--刷新按钮-->
      <nm-button icon="refresh" @click="refresh"/>
    </template>
    <slot/>
  </nm-dialog>
</template>
<script>
import dialog from '../../mixins/components/dialog.js'
export default {
  name: 'ListDialog',
  mixins: [dialog],
  data () {
    return {
      on: {
        open: this.onOpen,
        opened: this.onOpened,
        close: this.onClose,
        closed: this.onClosed
      }
    }
  },
  props: {
    /** 标题 */
    title: String,
    /** 图标 */
    icon: String,
    /** Dialog 的宽度 */
    width: {
      type: String,
      default: '60%'
    },
    /** Dialog 的高度 */
    height: {
      type: [Number, String],
      default: '80%'
    },
    /** 是否显示全屏按钮 */
    fullscreen: {
      type: Boolean,
      default: true
    },
    /** 是否可以通过点击 modal 关闭 Dialog */
    closeOnClickModal: {
      type: Boolean,
      default: false
    },
    /** 是否可拖拽 */
    draggable: {
      type: Boolean,
      default: false
    }
  },
  computed: {
    dialog () {
      return {
        noFooter: true,
        noScrollbar: true,
        title: this.title,
        icon: this.icon,
        width: this.width,
        height: this.height,
        fullscreen: this.fullscreen,
        closeOnClickModal: this.closeOnClickModal
      }
    }
  },
  methods: {
    refresh () {
      this.$slots.default.map(slot => {
        if (slot.componentOptions.tag === 'nm-list') {
          slot.componentInstance.refresh()
        }
      })
    },
    onOpen () {
      this.$emit('open')
    },
    onOpened () {
      this.$emit('opened')
    },
    onClose () {
      this.$emit('close')
    },
    onClosed () {
      this.$emit('closed')
    }
  }
}
</script>
