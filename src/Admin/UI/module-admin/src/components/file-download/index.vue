<template>
  <el-button
    class="nm-button nm-file-download"
    :type="type"
    :size="size || fontSize"
    :plain="plain"
    :round="round"
    :circle="circle"
    :loading="loading"
    :disabled="disabled"
    :autofocus="autofocus"
    :native-type="nativeType"
    v-nm-has="code"
    @click="onClick"
  >
    <nm-icon v-if="!loading && icon" :name="icon" />
    <slot>
      <span v-if="!circle && text" class="nm-button-text" v-html="text" />
    </slot>
  </el-button>
</template>
<script>
export default {
  props: {
    /** 文件链接 */
    url: String,
    /** 私有的 */
    private: Boolean,
    /** 文件名称 */
    fileName: String,
    /** 尺寸，默认按照框架的字号设置 */
    size: String,
    /** 类型 primary/success/warning/danger/info/text */
    type: String,
    /** 是否朴素按钮 */
    plain: Boolean,
    /** 是否圆角按钮 */
    round: Boolean,
    /** 是否圆形按钮 */
    circle: Boolean,
    /** 是否加载中状态 */
    loading: Boolean,
    /** 是否禁用状态 */
    disabled: Boolean,
    /** 是否默认聚焦 */
    autofocus: Boolean,
    /** 原生 type 属性 button/submit/reset */
    nativeType: String,
    /** 图标 */
    icon: String,
    // 文本
    text: String,
    // 按钮编码，用于按钮权限控制
    code: String
  },
  methods: {
    onClick() {
      if (this.url) {
        if (this.private) {
          $api.admin.file.download(this.url)
        } else {
          //通过模拟a标签点击事件下载文件
          const link = document.createElement('a')
          link.href = this.url
          let fileName = this.fileName
          if (!fileName) {
            let arr = this.url.split('/')
            fileName = arr[arr.length - 1]
          }
          link.setAttribute('download', fileName)
          link.setAttribute('target', '_blank')
          document.body.appendChild(link)
          link.click()
          document.body.removeChild(link)
        }
      }
      this.$emit('click')
    }
  }
}
</script>
