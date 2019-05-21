<template>
  <div class="nm-upload-single">
    <div class="input">
      <el-input :value="fileName" disabled/>
    </div>
    <div class="button">
      <el-upload ref="upload" v-bind="upload">
        <nm-button :type="btnType" text="上传" :icon="icon_" @click="onClick" :loading="loading"/>
      </el-upload>
    </div>
  </div>
</template>
<script>
import { mapState } from 'vuex'
export default {
  name: 'UploadSingle',
  data () {
    return {
      fileName: this.name,
      filePath: this.value,
      loading: false
    }
  },
  props: {
    value: String,
    name: String,
    /** 按钮类型 */
    btnType: {
      type: String,
      default: 'primary'
    },
    /** 不显示按钮 */
    noIcon: Boolean,
    /** 上传地址 */
    action: {
      type: String,
      required: true
    },
    /** 额外的数据 */
    data: Object,
    /** 文件最大值 */
    maxSize: String,
    /** 接受上传的文件类型 */
    accept: String
  },
  computed: {
    ...mapState('module/admin/token', ['accessToken']),
    maxSizeBytes () {
      if (this.maxSize) {
        const max = this.maxSize.toLowerCase()
        if (max.endsWith('kb')) {
          return parseFloat(max.replace('kb', '')) * 1024
        } else if (max.endsWith('mb')) {
          return parseFloat(max.replace('mb', '')) * 1024 * 1024
        } else if (max.endsWith('gb')) {
          return parseFloat(max.replace('gb', '')) * 1024 * 1024 * 1024
        } else if (max.endsWith('tb')) {
          return parseFloat(max.replace('tb', '')) * 1024 * 1024 * 1024 * 1024
        } else if (max.endsWith('b')) {
          return parseFloat(max.replace('b', ''))
        } else {
          return parseFloat(max)
        }
      }
      return 0
    },
    icon_ () {
      return this.loading ? 'loading' : this.noIcon ? '' : 'upload'
    },
    upload () {
      return {
        action: this.action,
        headers: {
          Authorization: 'Bearer ' + this.accessToken
        },
        multiple: false,
        data: this.data,
        accept: this.accept,
        limit: 1,
        drag: false,
        disabled: this.loading,
        autoUpload: true,
        showFileList: false,
        onSuccess: this.onSuccess,
        onError: this.onError,
        beforeUpload: this.onBeforeUpload
      }
    }
  },
  methods: {
    reset () {
      this.fileName = ''
      this.filePath = ''
    },
    onClick () {
      if (this.loading) { return }
      this.$refs.upload.clearFiles()
    },
    onBeforeUpload (file) {
      if (this.maxSizeBytes && file.size > this.maxSizeBytes) {
        this._error('文件大小不能超过' + this.maxSize)
        return false
      }
      this.loading = true
    },
    onSuccess (response, file, fileList) {
      if (response.code === 1) {
        this.fileName = response.data.fileName
        this.filePath = response.data.fullPath
        this.$emit('input', this.filePath)
        this.$emit('update:name', this.fileName)
        this.$emit('success', response.data, file)
      } else {
        this.onError()
      }
      this.loading = false
    },
    onError () {
      this._error('上传失败')
      this.loading = false
    }
  },
  watch: {
    value (val) {
      this.filePath = val
    },
    name (val) {
      this.fileName = val
    }

  }
}
</script>
