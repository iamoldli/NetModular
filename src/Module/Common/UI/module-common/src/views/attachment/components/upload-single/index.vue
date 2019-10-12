<template>
  <div class="nm-attachment-upload-single">
    <div class="input">
      <el-input :value="fileName" readonly />
    </div>
    <div class="button">
      <el-upload ref="upload" v-bind="uploadOptions">
        <nm-button :type="btnType" text="上传" :icon="icon_" @click="onClick" :loading="loading" />
      </el-upload>
    </div>
  </div>
</template>
<script>
import mixins from '../upload-mixins'

// 接口
const api = $api.common.attachment

export default {
  mixins: [mixins],
  data() {
    return {
      fileName: ''
    }
  },
  props: {
    /** 按钮类型 */
    btnType: {
      type: String,
      default: 'primary'
    },
    /** 不显示按钮 */
    noIcon: Boolean
  },
  computed: {
    uploadOptions() {
      return {
        action: api.getUploadUrl(),
        headers: {
          Authorization: 'Bearer ' + this.accessToken
        },
        data: {
          module: this.module,
          group: this.group,
          name: this.name,
          auth: this.auth
        },
        accept: this.accept,
        drag: false,
        autoUpload: true,
        showFileList: false,
        beforeUpload: this.onBeforeUpload,
        onSuccess: this.onSuccess,
        onError: this.onError
      }
    },
    icon_() {
      return this.loading ? 'loading' : this.noIcon ? '' : 'upload'
    }
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
      const data = res.data
      if (data.code === 1) {
        this.id = data.data.id
        this.fileName = file.name
        this.$emit('input', this.id)
        this.$emit('success', res, file)
      } else {
        this.$emit('error')
      }
      this.loading = false
    },
    reset() {
      this.id = ''
      this.fileName = ''
      this.$emit('input', this.id)
    },
    onClick() {
      if (this.loading) { return }
      this.$refs.upload.clearFiles()
    }
  },
  watch: {
    name(val) {
      this.fileName = val
    }
  }
}
</script>

<style lang="scss">
.nm-attachment-upload-single {
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
