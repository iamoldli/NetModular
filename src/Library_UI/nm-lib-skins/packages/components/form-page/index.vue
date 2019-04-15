<template>
  <nm-box class="nm-form-page" v-bind="box">
    <section class="nm-form-page-body">
      <nm-scrollbar :horizontal="false">
        <nm-form class="nm-form-page-main" ref="form" v-bind="form" v-on="formOn">
          <slot/>
        </nm-form>
      </nm-scrollbar>
    </section>

    <!--底部-->
    <template v-slot:footer class="nm-form-page-footer">
      <div class="nm-form-page-footer-left">
        <slot name="footer-left"/>
      </div>
      <div class="nm-form-page-footer-right">
        <slot name="fotter">
          <el-button v-if="btnOk" :type="btnOkType" @click="submit" :size="fontSize">{{btnOkText}}</el-button>
          <el-button v-if="btnReset" type="info" @click="reset" :size="fontSize">重置</el-button>
        </slot>
      </div>
    </template>
  </nm-box>
</template>
<script>
export default {
  name: 'FormPage',
  data () {
    return {
      loading: false,
      formOn: {
        success: this.onSuccess,
        error: this.onError,
        'validate-error': this.onValidateError
      }
    }
  },
  props: {
    /** 标题 */
    title: String,
    /** 显示头部 */
    header: {
      type: Boolean,
      default: true
    },
    /** box图标 */
    icon: {
      type: String,
      default: 'detail'
    },
    /** 表单 */
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
    // 自定义验证
    validate: Function,
    /** 标签的宽度 */
    labelWidth: String,
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
    /** 是否显示全屏按钮 */
    fullscreen: {
      type: Boolean,
      default: true
    }
  },
  computed: {
    box () {
      return {
        page: true,
        title: this.title,
        icon: this.icon,
        header: this.header,
        footer: true,
        toolbar: this.toolbar,
        loading: this.loading
      }
    },
    form () {
      return {
        noLoading: true,
        model: this.model,
        rules: this.rules,
        action: this.action,
        labelWidth: this.labelWidth,
        validate: this.validate,
        successMsg: this.successMsg,
        successMsgText: this.successMsgText
      }
    },
    toolbar () {
      let toolbar = []
      if (this.fullscreen === true) {
        toolbar.push('fullscreen')
      }
      return toolbar
    }
  },
  methods: {
    /** 提交 */
    submit () {
      this.loading = true
      this.$refs.form.submit()
    },
    /** 重置 */
    reset () {
      this.$refs.form.reset()
    },
    /** 打开loading */
    openLoading () {
      this.loading = true
      this.$refs.form.openLoading()
    },
    /** 关闭loading */
    closeLoading () {
      this.loading = false
      this.$refs.form.closeLoading()
    },
    // 成功
    onSuccess (data) {
      this.loading = false
      this.$emit('success', data)
    },
    onError () {
      this.loading = false
    },
    onValidateError () {
      this.loading = false
    }
  }
}
</script>
