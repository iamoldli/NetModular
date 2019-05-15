<template>
  <nm-dialog
    ref="dialog"
    class="nm-details-dialog"
    :title="title"
    :icon="icon"
    :width="width"
    :height="height"
    :fullscreen="fullscreen"
    :visible.sync="visible_"
    @open="$emit('open')"
    @opened="$emit('opened')"
    @close="$emit('close')"
    @closed="$emit('closed')"
  >
    <nm-details ref="details" :action="action" :label-width="labelWidth">
      <template slot-scope="{model}">
        <slot :model="model"/>
      </template>
    </nm-details>
  </nm-dialog>
</template>
<script>
import dialog from '../../mixins/components/dialog.js'
export default {
  name: 'DetailsDialog',
  mixins: [dialog],
  props: {
    /** 标题，也可通过具名title传入 */
    title: String,
    /** 图标 */
    icon: String,
    /** 宽度 */
    width: String,
    /** Dialog 的高度 */
    height: [Number, String],
    /** 是否显示全屏按钮 */
    fullscreen: Boolean,
    /** 标签的宽度 */
    labelWidth: {
      type: String,
      default: '100px'
    },
    action: {
      type: Function,
      required: true
    }
  },
  watch: {
    visible_() {
      if (this.visible_) {
        this.$nextTick(() => {
          this.$refs.details.query()
        })
      }
    }
  }
}
</script>
