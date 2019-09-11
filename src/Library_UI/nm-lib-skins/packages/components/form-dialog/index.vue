<template>
  <nm-dialog ref="dialog" class="nm-form-dialog" v-bind="dialog" v-on="dialogOn" :visible.sync="visible_">
    <nm-form ref="form" v-bind="form" v-on="formOn">
      <slot />
    </nm-form>

    <template v-slot:footer-left>
      <slot name="footer-left" />
    </template>

    <template v-slot:footer>
      <slot name="footer-buttons" />
      <slot name="footer">
        <el-button v-if="btnOk" :type="btnOkType" @click="submit" :size="fontSize">{{btnOkText}}</el-button>
        <el-button v-if="btnReset" type="info" @click="reset" :size="fontSize">重置</el-button>
      </slot>
    </template>
  </nm-dialog>
</template>
<script>
import dialog from '../../mixins/components/dialog.js'
export default {
  name: 'FormDialog',
  mixins: [dialog],
  data() {
    return {
      loading_: false,
      formOn: {
        success: this.onSuccess,
        error: this.onError,
        'validate-error': this.onValidateError
      },
      dialogOn: {
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
    /** 宽度 */
    width: String,
    /** Dialog 的高度 */
    height: [Number, String],
    /** 显示尾部 */
    footer: {
      type: Boolean,
      default: true
    },
    /** 是否可以通过点击 modal 关闭 Dialog */
    closeOnClickModal: {
      type: Boolean,
      default: true
    },
    /** 是否显示全屏按钮 */
    fullscreen: Boolean,
    /** 表单模型 */
    model: {
      type: Object,
      required: true
    },
    /** 验证规则 */
    rules: Object,
    /** 提交请求 */
    action: {
      type: Function,
      required: true
    },
    /** 标签的宽度 */
    labelWidth: String,
    /** 表单域标签的位置，如果值为 left 或者 right 时，则需要设置 label-width */
    labelPosition: String,
    // 自定义验证
    validate: Function,
    /** 是否显示成功提示消息 */
    successMsg: {
      type: Boolean,
      default: true
    },
    /** 成功提示消息文本 */
    successMsgText: {
      type: String,
      default: '保存成功'
    },
    /** Ok按钮 */
    btnOk: {
      type: Boolean,
      default: true
    },
    /** Ok按钮文本 */
    btnOkText: {
      type: String,
      default: '保存'
    },
    /** Ok按钮类型 */
    btnOkType: {
      type: String,
      default: 'primary'
    },
    /** reset按钮 */
    btnReset: {
      type: Boolean,
      default: true
    },
    /** 自定义重置操作 */
    customResetFunction: Function,
    // 保存成功后是否关闭对话框
    closeWhenSuccess: {
      type: Boolean,
      default: true
    },
    /** 禁用表单 */
    disabled: Boolean,
    /** 显示加载动画 */
    loading: Boolean,
    /** 不显示加载动画 */
    noLoading: {
      type: Boolean,
      default: false
    },
    /** 打开时是否清楚验证信息 */
    clearValidateOnOpen: {
      type: Boolean,
      default: true
    },
    /** 是否显示底部关闭按钮 */
    footerCloseButton: Boolean
  },
  computed: {
    dialog() {
      return {
        title: this.title,
        icon: this.icon,
        width: this.width,
        height: this.height,
        footer: this.footer,
        fullscreen: this.fullscreen,
        closeOnClickModal: this.closeOnClickModal,
        loading: this.showLoading,
        footerCloseButton: this.footerCloseButton
      }
    },
    form() {
      return {
        noLoading: true,
        model: this.model,
        rules: this.rules,
        action: this.action,
        labelWidth: this.labelWidth,
        labelPosition: this.labelPosition,
        validate: this.validate,
        successMsg: this.successMsg,
        successMsgText: this.successMsgText,
        disabled: this.disabled
      }
    },
    showLoading() {
      return !this.noLoading && (this.loading_ || this.loading)
    }
  },
  methods: {
    /** 提交 */
    submit() {
      this.loading_ = true
      this.$refs.form.submit()
    },
    /** 重置 */
    reset() {
      if (this.customResetFunction) {
        this.customResetFunction()
      } else {
        this.$refs.form.reset()
      }
    },
    /** 清除验证信息 */
    clearValidate() {
      this.$refs.form.clearValidate()
    },
    /** 打开loading */
    openLoading() {
      this.loading_ = true
    },
    /** 关闭loading */
    closeLoading() {
      this.loading = false
    },
    // 成功
    onSuccess(data) {
      // 关闭对话框
      if (this.closeWhenSuccess) {
        setTimeout(this.hide, 800)
      }
      this.loading_ = false
      this.$emit('success', data)
    },
    onError() {
      this.loading_ = false
      this.$emit('error')
    },
    onValidateError() {
      this.loading_ = false
      this.$emit('validate-error')
    },
    onOpen() {
      if (this.clearValidateOnOpen) {
        this.$nextTick(() => {
          this.$refs.form.clearValidate()
        })
      }
      this.$emit('open')
    },
    onOpened() {
      this.$emit('opened')
    },
    onClose() {
      this.$emit('close')
    },
    onClosed() {
      this.$emit('closed')
    }
  }
}
</script>
