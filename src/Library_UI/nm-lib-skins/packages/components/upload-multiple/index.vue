<template>
  <nm-box class="nm-upload-multiple" :loading="loading">
    <el-upload ref="upload" v-bind="upload">
      <div>
        <i class="el-icon-upload"></i>
        <div class="el-upload__text">
          将文件拖到此处，或
          <em>点击上传</em>
        </div>
      </div>
      <div v-if="tip" class="nm-upload-multiple-tip">
        <slot name="tip">
          <i class="el-alert__icon el-icon-warning"></i>
          {{tip}}
        </slot>
      </div>
    </el-upload>
    <el-table v-if="showTable" :data="fileList" border size="mini" style="width: 100%">
      <el-table-column align="center" type="index" width="50"></el-table-column>
      <el-table-column align="center" prop="name" label="名称"></el-table-column>
      <el-table-column align="center" prop="size" label="大小" width="180"></el-table-column>
      <el-table-column align="center" prop="ext" label="扩展" width="180"></el-table-column>
      <el-table-column align="center" label="操作">
        <template v-slot:default="{row}">
          <nm-button type="text" icon="delete" text="删除" @click="onRemove(row)" />
        </template>
      </el-table-column>
    </el-table>
  </nm-box>
</template>
<script>
import { mapState } from 'vuex'
import loadingMixins from '../../mixins/components/loading'
export default {
  name: 'UploadMultiple',
  mixins: [loadingMixins],
  data() {
    return {
      fileList: [],
      loading: false
    }
  },
  props: {
    value: Array,
    /** 必选参数，上传的地址 */
    action: {
      type: String,
      required: true
    },
    /** 是否启用多选，该属性是控制打开系统的文件选择对话框时，是否可以选中多个，不是上传多个文件，上传单个或多个文件使用limit属性 */
    multiple: {
      type: Boolean,
      default: true
    },
    /** 上传时附带的额外参数 */
    data: Object,
    /** 接受上传的文件类型 */
    accept: String,
    /** 最大允许上传个数 */
    limit: Number,
    /** 文件最大大小 */
    maxSize: String,
    /** 是否可拖拽上传 */
    drag: {
      type: Boolean,
      default: true
    },
    /** 提示信息 */
    tip: String,
    /** 始终显示文件列表 */
    alwaysShowTable: Boolean
  },
  computed: {
    ...mapState('app/token', ['accessToken']),
    upload() {
      return {
        action: this.action,
        headers: {
          Authorization: 'Bearer ' + this.accessToken
        },
        multiple: true,
        data: this.data,
        accept: this.accept,
        limit: this.limit,
        drag: this.drag,
        showFileList: false,
        beforeUpload: this.onBeforeUpload,
        onSuccess: this.onSuccess,
        onError: this.onError,
        onExceed: this.onExceed
      }
    },
    maxSizeBytes() {
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
      return ''
    },
    showTable() {
      return this.alwaysShowTable || (this.fileList && this.fileList.length > 0)
    }
  },
  methods: {
    clearFiles() {
      this.$refs.upload.clearFiles()
    },
    reset() {
      this.fileList = []
    },
    /** 远程文件对象转换 */
    convert(file, localFile) {
      return {
        name: file.fileName,
        size: this.getSizeUnit(file.size),
        ext: file.ext,
        localFile: localFile,
        remoteFile: file
      }
    },
    /** 上传 */
    submit() {
      this.$refs.upload.submit()
    },
    onBeforeUpload(file) {
      // 验证是否超出最大上传数
      if (this.limit && this.fileList.length > this.limit) {
        this.$refs.upload.handleRemove(file)
        this.onExceed()
        return false
      }
      if (this.maxSizeBytes && file.size > this.maxSizeBytes) {
        this._error('文件大小不能超过' + this.maxSize)
        return false
      }
      this.loading = true
    },
    onSuccess(response, file, fileList) {
      for (let i = 0; i < this.fileList.length; i++) {
        let $file = this.fileList[i]
        if ($file.localFile === file) {
          if (response.code === 1) {
            $file.status = 1
          } else {
            $file.status = 2
          }
          if (i === this.fileList.length - 1) {
            this.loading = false
            this.$emit('success')
          }
        }
      }
      if (response.code === 1) {
        response.data.map(f => {
          this.fileList.push(this.convert(f, file))
        })
        this.$emit('input', this.fileList.map(m => m.remoteFile))
        this.loading = false
      } else {
        this.onError()
      }
    },
    onError() {
      this._error('上传失败')
      this.loading = false
    },
    onRemove(file) {
      if (file.localFile) {
        this.$refs.upload.handleRemove(file.localFile)
      }
      let fileList = this.fileList
      fileList.splice(fileList.indexOf(file), 1)
    },
    /** 文件超出 */
    onExceed() {
      this._warning(`最多允许上传${this.limit}个`)
    },
    /** 获取文件后缀名 */
    getFileExt(name) {
      let ext = ''
      const arr = name.split('.')
      if (arr.length > 1) {
        ext = arr[arr.length - 1]
      }
      return ext
    },
    /** 转换文件大小单位 */
    getSizeUnit(size) {
      if (size === null || size === '') {
        return '0 B'
      }
      var unitArr = ['B', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB']
      var index = 0
      var srcsize = parseFloat(size)
      index = Math.floor(Math.log(srcsize) / Math.log(1024))
      var s = srcsize / Math.pow(1024, index)
      s = s.toFixed(2)
      return s + unitArr[index]
    }
  }
}
</script>
