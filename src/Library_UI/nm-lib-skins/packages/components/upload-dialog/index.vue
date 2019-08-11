<template>
  <nm-dialog ref="form" class="nm-upload-dialog" v-bind="dialog" :visible.sync="visible_" @close="onClose">
    <div class="nm-upload-dialog-top">
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
    </div>
    <div class="nm-upload-dialog-bottom">
      <el-table :data="fileList" border size="mini" height="100%" style="width: 100%;">
        <el-table-column align="center" type="index" width="50"></el-table-column>
        <el-table-column align="center" prop="name" label="名称"></el-table-column>
        <el-table-column align="center" prop="size" label="大小" width="180"></el-table-column>
        <el-table-column align="center" prop="ext" label="扩展" width="180"></el-table-column>
        <el-table-column align="center" label="操作">
          <template v-slot:default="{row}">
            <nm-icon class="status-success" v-if="row.status===1" name="select" />
            <nm-icon class="status-error" v-else-if="row.status===2" name="close" />
            <nm-button type="text" icon="delete" text="删除" @click="onRemove(row)" />
          </template>
        </el-table-column>
      </el-table>
    </div>
    <template v-slot:footer>
      <nm-button type="success" text="保存" @click="onSubmit" />
    </template>
  </nm-dialog>
</template>
<script>
import { mapState } from 'vuex'
import dialog from '../../mixins/components/dialog.js'

export default {
  name: 'UploadDialog',
  mixins: [dialog],
  data() {
    return {
      fileList: [],
      loading: false
    }
  },
  props: {
    /** 标题 */
    title: {
      type: String,
      default: '文件上传'
    },
    /** 图标 */
    icon: {
      type: String,
      default: 'upload'
    },
    /** 宽度 */
    width: {
      type: String,
      default: '50%'
    },
    /** 高度 */
    height: {
      type: String,
      default: '80%'
    },
    /** 必选参数，上传的地址 */
    action: {
      type: String,
      required: true
    },
    /** 上传时附带的额外参数 */
    data: Object,
    /** 接受上传的文件类型 */
    accept: String,
    /** 底部提示语 */
    tip: String,
    /** 最大允许上传个数 */
    limit: Number,
    /** 文件最大大小 */
    maxSize: String,
    /** 是否可拖拽上传 */
    drag: {
      type: Boolean,
      default: true
    },
    /** 关闭对话框时进行重置 */
    resetOnClose: {
      type: Boolean,
      default: true
    }
  },
  computed: {
    ...mapState('app/token', ['accessToken']),
    dialog() {
      return {
        title: this.title,
        icon: this.icon,
        width: this.width,
        height: this.height,
        loading: this.loading,
        footer: true,
        noScrollbar: true
      }
    },
    upload() {
      return {
        action: this.action,
        headers: {
          Authorization: 'Bearer ' + this.accessToken
        },
        data: this.data,
        accept: this.accept,
        limit: this.limit,
        drag: this.drag,
        multiple: true,
        autoUpload: false,
        showFileList: false,
        beforeUpload: this.onBeforeUpload,
        onChange: this.onChange,
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
    }
  },
  methods: {
    clearFiles() {
      this.$refs.upload.clearFiles()
    },
    onClose() {
      if (this.resetOnClose) {
        this.$nextTick(() => {
          this.clearFiles()
        })
      }
    },
    onSubmit() {
      // 验证是否超出最大上传数
      if (this.limit && this.fileList.length > this.limit) {
        this.onExceed()
        return
      }
      if (this.maxSizeBytes) {
        for (let i = 0; i < this.fileList.length; i++) {
          const file = this.fileList[i].localFile
          if (file.size > this.maxSizeBytes) {
            this._error(`文件《${file.name}》的大小超过了最大允许值${this.maxSize}`)
            return
          }
        }
      }
      this.$refs.upload.submit()
    },
    onChange(file, fileList) {
      if (this.fileList.length > 0 && fileList.length === this.fileList.length) { return }
      this.fileList = []
      fileList.map(m => {
        this.fileList.push({
          name: m.name,
          ext: this.getFileExt(m.name),
          size: this.getSizeUnit(m.size),
          status: 0,
          localFile: m
        })
      })
    },
    onBeforeUpload(file) {
      this.loading = true
    },
    onSuccess(response, file) {
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
    },
    onError() {
      this._error('上传失败')
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
