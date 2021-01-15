<template>
  <div class="nm-file-upload">
    <div class="input">
      <el-input :value="fileName" readonly :disabled="disabled" />
    </div>
    <div class="button">
      <el-upload
        ref="upload"
        :action="uploadAction"
        :headers="headers"
        :data="uploadData"
        name="formFile"
        :disabled="disabled || loading"
        :before-upload="onBeforeUpload"
        :show-file-list="false"
        :auto-upload="autoUpload"
        :accept="accept"
        :drag="false"
        :on-success="onSuccess"
        :on-error="onError"
      >
        <nm-button :type="btnType" text="上传" :icon="loading ? 'loading' : noIcon ? '' : 'upload'" :loading="loading" :disabled="disabled" />
      </el-upload>
    </div>
  </div>
</template>
<script>
import mixins from '../upload-mixins'
export default {
  mixins: [mixins],
  data() {
    return {
      value_: this.value,
      fileName: ''
    }
  },
  props: {
    value: {
      type: String
    },
    /** 按钮类型 */
    btnType: {
      type: String,
      default: 'primary'
    },
    /** 不显示按钮图标 */
    noIcon: Boolean
  },
  methods: {
    onBeforeUpload(file) {
      // 文件格式验证
      if (!this.verifyFileType(file)) {
        return false
      }
      // 验证是否超出最大上传数
      if (this.maxSizeBytes && file.size > this.maxSizeBytes) {
        this._error(`文件大小${this.getSizeUnit(file.size)}超过了最大允许值${this.maxSize}`)
        return false
      }
      if (this.beforeUpload && typeof this.beforeUpload === 'function' && !this.beforeUpload(file)) {
        return false
      }

      this.loading = true
      return true
    },
    onSuccess(res, file) {
      if (res.code === 1) {
        const data = res.data
        this.value_ = data.fullPath
        this.fileName = file.name
        this.$emit('input', this.value_)
        this.$emit('success', data, file)
      } else {
        this._error(res.msg)
        this.$emit('error')
      }
      this.loading = false
    },
    reset() {
      this.value_ = ''
      this.fileName = ''
      this.$emit('input', this.value_)
    },
    get() {
      if (this.value_) {
        $api.admin.file.getByFullPath(this.value_).then(data => {
          this.fileName = data.fileName
        })
      } else {
        this.fileName = ''
      }
    }
  },
  watch: {
    value: {
      immediate: true,
      handler(val) {
        if (val !== this.value_) {
          this.value_ = val
          this.get()
        }
      }
    }
  }
}
</script>

<style lang="scss">
.nm-file-upload {
  position: relative;
  padding-right: 80px;
  width: 100%;
  box-sizing: border-box;
  .input {
    .el-input__inner {
      border-top-right-radius: 0;
      border-bottom-right-radius: 0;
    }
  }
  .button {
    position: absolute;
    right: 0;
    top: 0;
    width: 80px;
    height: 100%;

    > div,
    .el-upload,
    .nm-button {
      height: 100%;
    }

    .nm-button {
      border-top-left-radius: 0;
      border-bottom-left-radius: 0;
    }
  }
}
</style>
